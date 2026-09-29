using HalachevAccounting.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HalachevAccounting.Controllers;

[ApiController]
[AllowAnonymous]
public sealed class HealthController(AppDbContext db) : ControllerBase
{
    [HttpGet("/health/live")]
    public IActionResult Live() => Ok(new { status = "live" });

    [HttpGet("/health/ready")]
    public async Task<IActionResult> Ready(CancellationToken cancellationToken)
    {
        bool database = await db.Database.CanConnectAsync(cancellationToken);
        return database
            ? Ok(new { status = "ready", database = true })
            : StatusCode(StatusCodes.Status503ServiceUnavailable, new { status = "unready", database = false });
    }
}
