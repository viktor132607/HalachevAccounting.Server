using HalachevAccounting.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HalachevAccounting.Controllers;

[Authorize(Roles = "Admin")]
[ApiController]
[Route("api/admin/service-requests")]
public class AdminServiceRequestsController : ControllerBase
{
	private readonly AppDbContext _context;

	public AdminServiceRequestsController(AppDbContext context)
	{
		_context = context;
	}

	[HttpGet]
	public async Task<IActionResult> GetAll()
	{
		var requests = await _context.ServiceRequests
			.Include(x => x.Service)
			.OrderByDescending(x => x.CreatedAt)
			.ToListAsync();

		return Ok(requests);
	}

	[HttpGet("{id}")]
	public async Task<IActionResult> Get(int id)
	{
		var request = await _context.ServiceRequests
			.Include(x => x.Service)
			.FirstOrDefaultAsync(x => x.Id == id);

		if (request == null)
			return NotFound();

		return Ok(request);
	}

	[HttpDelete("{id}")]
	public async Task<IActionResult> Delete(int id)
	{
		var request = await _context.ServiceRequests.FindAsync(id);

		if (request == null)
			return NotFound();

		_context.ServiceRequests.Remove(request);
		await _context.SaveChangesAsync();

		return Ok();
	}
}