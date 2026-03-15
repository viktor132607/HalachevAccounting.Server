using HalachevAccounting.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using HalachevAccounting.Domain.Entities;

namespace HalachevAccounting.Controllers;

[Authorize(Roles = "Admin")]
[ApiController]
[Route("api/admin/dashboard")]
public class AdminDashboardController : ControllerBase
{
	private readonly AppDbContext _context;
	private readonly UserManager<ApplicationUser> _userManager;

	public AdminDashboardController(
		AppDbContext context,
		UserManager<ApplicationUser> userManager)
	{
		_context = context;
		_userManager = userManager;
	}

	[HttpGet]
	public async Task<IActionResult> GetStats()
	{
		var users = await _userManager.Users.CountAsync();
		var contacts = await _context.ContactRequests.CountAsync();
		var serviceRequests = await _context.ServiceRequests.CountAsync();
		var blogPosts = await _context.BlogPosts.CountAsync();

		return Ok(new
		{
			users,
			contacts,
			serviceRequests,
			blogPosts
		});
	}
}