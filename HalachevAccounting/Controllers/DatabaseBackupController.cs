using HalachevAccounting.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HalachevAccounting.Controllers;

[ApiController]
[Authorize(Roles = "Admin")]
[Route("api/admin/database-backup")]
[ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
public sealed class DatabaseBackupController(
    DatabaseBackupService service,
    ILogger<DatabaseBackupController> logger) : ControllerBase
{
    [HttpGet("export")]
    public async Task<IActionResult> Export(CancellationToken cancellationToken)
    {
        DatabaseBackupArtifact backup = await service.CreateBackupAsync(cancellationToken);
        FileStream stream = new(
            backup.FilePath,
            FileMode.Open,
            FileAccess.Read,
            FileShare.Read,
            128 * 1024,
            FileOptions.Asynchronous | FileOptions.SequentialScan | FileOptions.DeleteOnClose);

        return File(stream, "application/octet-stream", backup.FileName);
    }

    [HttpPost("restore")]
    [DisableRequestSizeLimit]
    public async Task<IActionResult> Restore(
        [FromForm] IFormFile? archive,
        [FromForm] string? confirmation,
        CancellationToken cancellationToken)
    {
        if (!string.Equals(confirmation, "RESTORE HALACHEV", StringComparison.Ordinal))
        {
            return BadRequest(new { message = "Type RESTORE HALACHEV to confirm." });
        }

        if (archive is null || archive.Length == 0)
        {
            return BadRequest(new { message = "A PostgreSQL backup archive is required." });
        }

        try
        {
            await using Stream input = archive.OpenReadStream();
            await service.RestoreAsync(input, cancellationToken);
        }
        catch (InvalidDataException exception)
        {
            return BadRequest(new { message = exception.Message });
        }

        logger.LogWarning(
            "Administrator restored full database archive {ArchiveName} ({ArchiveSize} bytes).",
            Path.GetFileName(archive.FileName),
            archive.Length);

        return Ok(new { message = "Database restored successfully.", restoredAtUtc = DateTime.UtcNow });
    }
}
