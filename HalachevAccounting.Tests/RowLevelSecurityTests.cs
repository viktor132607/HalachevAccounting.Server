using HalachevAccounting.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using ReparoNow.Domain.Entities;

namespace HalachevAccounting.Tests;

public sealed class RowLevelSecurityTests
{
    [Fact]
    public async Task AnonymousApplicationRoleCannotReadContactRequestsButAdminContextCan()
    {
        string? rootConnection = Environment.GetEnvironmentVariable("HALACHEV_TEST_POSTGRES");
        if (string.IsNullOrWhiteSpace(rootConnection)) return;

        string suffix = Guid.NewGuid().ToString("N");
        string databaseName = "halachev_rls_" + suffix;
        string roleName = "halachev_app_" + suffix;
        string rolePassword = "Test_" + suffix + "!";

        await using NpgsqlConnection admin = new(rootConnection);
        await admin.OpenAsync();
        await ExecuteAsync(admin, $"CREATE DATABASE \"{databaseName}\"");
        await ExecuteAsync(
            admin,
            $"CREATE ROLE \"{roleName}\" LOGIN PASSWORD '{rolePassword}' NOSUPERUSER NOCREATEDB NOCREATEROLE NOINHERIT NOBYPASSRLS");

        NpgsqlConnectionStringBuilder ownerBuilder = new(rootConnection)
        {
            Database = databaseName,
            Pooling = false
        };

        try
        {
            DbContextOptions<AppDbContext> options =
                new DbContextOptionsBuilder<AppDbContext>()
                    .UseNpgsql(ownerBuilder.ConnectionString)
                    .Options;

            await using (AppDbContext context = new(options))
            {
                await context.Database.MigrateAsync();
                context.ContactRequests.Add(new ContactRequest
                {
                    Id = Guid.NewGuid(),
                    Name = "Private",
                    Email = "private@example.test",
                    Message = "Private message"
                });
                await context.SaveChangesAsync();
            }

            await using (NpgsqlConnection owner = new(ownerBuilder.ConnectionString))
            {
                await owner.OpenAsync();
                await ExecuteAsync(
                    owner,
                    $"""
                    GRANT CONNECT ON DATABASE "{databaseName}" TO "{roleName}";
                    GRANT USAGE ON SCHEMA public TO "{roleName}";
                    GRANT SELECT, INSERT ON "ContactRequests" TO "{roleName}";
                    """);
            }

            NpgsqlConnectionStringBuilder appBuilder = new(ownerBuilder.ConnectionString)
            {
                Username = roleName,
                Password = rolePassword
            };

            await using NpgsqlConnection app = new(appBuilder.ConnectionString);
            await app.OpenAsync();

            await SetContextAsync(app, false, false);
            Assert.Equal("0", await ScalarAsync(app, """SELECT count(*) FROM "ContactRequests";"""));

            await SetContextAsync(app, true, false);
            Assert.Equal("1", await ScalarAsync(app, """SELECT count(*) FROM "ContactRequests";"""));
        }
        finally
        {
            await ExecuteAsync(admin, $"DROP DATABASE \"{databaseName}\" WITH (FORCE)");
            await ExecuteAsync(admin, $"DROP ROLE IF EXISTS \"{roleName}\"");
        }
    }

    private static async Task SetContextAsync(NpgsqlConnection connection, bool isAdmin, bool isSystem)
    {
        await using NpgsqlCommand command = new(
            """
            SELECT
                set_config('app.current_user_id', '', false),
                set_config('app.current_is_admin', @admin, false),
                set_config('app.current_is_system', @system, false);
            """,
            connection);
        command.Parameters.AddWithValue("admin", isAdmin ? "true" : "false");
        command.Parameters.AddWithValue("system", isSystem ? "true" : "false");
        await command.ExecuteNonQueryAsync();
    }

    private static Task<int> ExecuteAsync(NpgsqlConnection connection, string sql) =>
        new NpgsqlCommand(sql, connection).ExecuteNonQueryAsync();

    private static async Task<string?> ScalarAsync(NpgsqlConnection connection, string sql) =>
        (await new NpgsqlCommand(sql, connection).ExecuteScalarAsync())?.ToString();
}
