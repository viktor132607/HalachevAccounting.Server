using HalachevAccounting.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReparoNow.Domain.Enums;

namespace HalachevAccounting.Controllers;

[Authorize(Roles = "Admin")]
[ApiController]
[Route("api/admin/contact-requests")]
public class AdminContactRequestsController : ControllerBase
{
	private readonly AppDbContext _context;

	public AdminContactRequestsController(AppDbContext context)
	{
		_context = context;
	}

	[HttpGet]
	public async Task<IActionResult> GetAll()
	{
		var requests = await _context.ContactRequests
			.OrderByDescending(x => x.CreatedAtUtc)
			.ToListAsync();

		return Ok(requests);
	}

	[HttpGet("{id}")]
	public async Task<IActionResult> Get(int id)
	{
		var request = await _context.ContactRequests.FindAsync(id);

		if (request == null)
			return NotFound();

		return Ok(request);
	}

	[HttpPut("{id}/comment")]
	public async Task<IActionResult> AddAdminComment(int id, [FromBody] string comment)
	{
		var request = await _context.ContactRequests.FindAsync(id);

		if (request == null)
			return NotFound();

		request.AdminComment = comment;

		await _context.SaveChangesAsync();

		return Ok();
	}

	[HttpPut("{id}/status")]
	public async Task<IActionResult> UpdateStatus(int id, [FromBody] string status)
	{
		var request = await _context.ContactRequests.FindAsync(id);

		if (request == null)
			return NotFound();

		if (!Enum.TryParse<ContactRequestStatus>(status, true, out var parsedStatus))
			return BadRequest("Invalid status.");

		request.Status = parsedStatus;

		await _context.SaveChangesAsync();

		return Ok();
	}
}