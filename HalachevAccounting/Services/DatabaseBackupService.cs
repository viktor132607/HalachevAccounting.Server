using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using Microsoft.Extensions.Logging.Abstractions;
using Npgsql;

namespace HalachevAccounting.Api.Services;

public sealed record DatabaseBackupArtifact(string FilePath, string FileName);

public sealed class DatabaseBackupService
{
    private const int CopyBufferSize = 128 * 1024;
    private const long DistributedBackupLockKey = 817430201;
    private static readonly byte[] PgDumpMagic = Encoding.ASCII.GetBytes("PGDMP");

    private readonly NpgsqlConnectionStringBuilder connection;
    private readonly ILogger<DatabaseBackupService> logger;
    private readonly PostgresAdvisoryLock distributedLock;
    private readonly SemaphoreSlim localLock = new(1, 1);

    public DatabaseBackupService(
        string connectionString,
        ILogger<DatabaseBackupService> logger)
        : this(
            connectionString,
            logger,
            new PostgresAdvisoryLock(connectionString, NullLogger<PostgresAdvisoryLock>.Instance))
    {
    }

    public DatabaseBackupService(
        string connectionString,
        ILogger<DatabaseBackupService> logger,
        PostgresAdvisoryLock distributedLock)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(connectionString);
        connection = new NpgsqlConnectionStringBuilder(connectionString);
        this.logger = logger;
        this.distributedLock = distributedLock;
    }

    public async Task<DatabaseBackupArtifact> CreateBackupAsync(CancellationToken cancellationToken = default)
    {
        await localLock.WaitAsync(cancellationToken);
        string? path = null;

        try
        {
            await using IAsyncDisposable lease =
                await distributedLock.AcquireAsync(DistributedBackupLockKey, cancellationToken);

            path = TemporaryPath("dump");
            await RunAsync(
                "pg_dump",
                ["--format=custom", "--compress=9", "--file", path, connection.Database!],
                "create database backup",
                cancellationToken);

            await ValidateHeaderAsync(path, cancellationToken);
            FileInfo file = new(path);
            if (!file.Exists || file.Length == 0)
            {
                throw new InvalidOperationException("PostgreSQL backup archive is empty.");
            }

            logger.LogInformation("Full PostgreSQL backup created. Size: {Size} bytes.", file.Length);
            return new(path, $"halachev-accounting-{DateTime.UtcNow:yyyyMMdd-HHmmss}Z.dump");
        }
        catch
        {
            if (path is not null) TryDelete(path);
            throw;
        }
        finally
        {
            localLock.Release();
        }
    }

    public async Task RestoreAsync(Stream archive, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(archive);
        await localLock.WaitAsync(cancellationToken);

        string? upload = null;
        string? sql = null;
        string? reset = null;

        try
        {
            upload = TemporaryPath("dump");
            await using (FileStream destination = new(
                upload,
                FileMode.CreateNew,
                FileAccess.Write,
                FileShare.None,
                CopyBufferSize,
                FileOptions.Asynchronous | FileOptions.SequentialScan))
            {
                await archive.CopyToAsync(destination, cancellationToken);
            }

            await ValidateHeaderAsync(upload, cancellationToken);
            await RunAsync("pg_restore", ["--list", upload], "validate database backup", cancellationToken);

            sql = TemporaryPath("sql");
            reset = TemporaryPath("sql");
            await RunAsync(
                "pg_restore",
                ["--clean", "--if-exists", "--no-owner", "--no-privileges", "--file", sql, upload],
                "materialize database backup",
                cancellationToken);

            await File.WriteAllTextAsync(reset, ResetSql, cancellationToken);

            await using IAsyncDisposable lease =
                await distributedLock.AcquireAsync(DistributedBackupLockKey, cancellationToken);

            NpgsqlConnection.ClearAllPools();
            try
            {
                await RunAsync(
                    "psql",
                    [
                        "--no-psqlrc",
                        "--no-password",
                        "--single-transaction",
                        "--set=ON_ERROR_STOP=on",
                        "--file", reset,
                        "--file", sql
                    ],
                    "restore database backup",
                    cancellationToken);
            }
            finally
            {
                NpgsqlConnection.ClearAllPools();
            }

            logger.LogWarning("Full PostgreSQL database restore completed.");
        }
        finally
        {
            if (upload is not null) TryDelete(upload);
            if (sql is not null) TryDelete(sql);
            if (reset is not null) TryDelete(reset);
            localLock.Release();
        }
    }

    private const string ResetSql = """
        SET LOCAL lock_timeout = '30s';
        SELECT pg_advisory_xact_lock(817430202);
        DO $reset$
        DECLARE item record;
        BEGIN
            FOR item IN SELECT extname FROM pg_extension WHERE extname <> 'plpgsql'
            LOOP EXECUTE format('DROP EXTENSION %I CASCADE', item.extname); END LOOP;
            FOR item IN SELECT nspname FROM pg_namespace
                WHERE nspname <> 'information_schema' AND nspname !~ '^pg_'
            LOOP EXECUTE format('DROP SCHEMA %I CASCADE', item.nspname); END LOOP;
            PERFORM lo_unlink(oid) FROM pg_largeobject_metadata;
        END $reset$;
        CREATE SCHEMA public;
        GRANT USAGE ON SCHEMA public TO PUBLIC;
        """;

    private async Task RunAsync(
        string executable,
        IReadOnlyCollection<string> arguments,
        string operation,
        CancellationToken cancellationToken)
    {
        ProcessStartInfo info = new()
        {
            FileName = executable,
            RedirectStandardError = true,
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        foreach (string argument in arguments) info.ArgumentList.Add(argument);

        info.Environment["PGCONNECT_TIMEOUT"] = "30";
        info.Environment["PGHOST"] = connection.Host;
        info.Environment["PGPORT"] = connection.Port.ToString();
        info.Environment["PGDATABASE"] = connection.Database;
        info.Environment["PGUSER"] = connection.Username;
        info.Environment["PGOPTIONS"] = "-c app.current_is_system=true -c app.current_is_admin=true";
        if (!string.IsNullOrWhiteSpace(connection.Password)) info.Environment["PGPASSWORD"] = connection.Password;

        string sslMode = connection.SslMode.ToString();
        info.Environment["PGSSLMODE"] = sslMode switch
        {
            "VerifyCA" => "verify-ca",
            "VerifyFull" => "verify-full",
            _ => sslMode.ToLowerInvariant()
        };

        using Process process = new() { StartInfo = info };
        try
        {
            if (!process.Start()) throw new InvalidOperationException($"Unable to start {executable}.");
        }
        catch (Win32Exception exception)
        {
            throw new InvalidOperationException($"PostgreSQL utility '{executable}' is unavailable.", exception);
        }

        Task output = process.StandardOutput.BaseStream.CopyToAsync(Stream.Null);
        Task<string> error = process.StandardError.ReadToEndAsync();
        await process.WaitForExitAsync(cancellationToken);
        await output;
        string stderr = await error;

        if (process.ExitCode != 0)
        {
            throw new InvalidOperationException(
                $"{executable} failed to {operation} with exit code {process.ExitCode}: {stderr.Trim()}");
        }
    }

    private static async Task ValidateHeaderAsync(string path, CancellationToken cancellationToken)
    {
        await using FileStream stream = File.OpenRead(path);
        byte[] header = new byte[PgDumpMagic.Length];
        int read = await stream.ReadAsync(header, cancellationToken);
        if (read != PgDumpMagic.Length || !header.SequenceEqual(PgDumpMagic))
        {
            throw new InvalidDataException("File is not a PostgreSQL custom-format archive.");
        }
    }

    private static string TemporaryPath(string extension) =>
        Path.Combine(Path.GetTempPath(), $"halachev-db-{Guid.NewGuid():N}.{extension}");

    private static void TryDelete(string path)
    {
        try { if (File.Exists(path)) File.Delete(path); }
        catch { }
    }
}
