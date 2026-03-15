using HalachevAccounting.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HalachevAccounting.Controllers;

[Authorize(Roles = "Admin")]
[ApiController]
[Route("api/admin/users")]
public class AdminUsersController : ControllerBase
{
	private readonly UserManager<ApplicationUser> _userManager;

	public AdminUsersController(UserManager<ApplicationUser> userManager)
	{
		_userManager = userManager;
	}

	[HttpGet]
	public IActionResult GetUsers()
	{
		var users = _userManager.Users
			.Select(x => new
			{
				x.Id,
				x.Email,
				x.IsBlocked
			})
			.ToList();

		return Ok(users);
	}

	[HttpPost("{id}/make-admin")]
	public async Task<IActionResult> MakeAdmin(string id)
	{
		var user = await _userManager.FindByIdAsync(id);

		if (user == null)
			return NotFound();

		if (!await _userManager.IsInRoleAsync(user, "Admin"))
			await _userManager.AddToRoleAsync(user, "Admin");

		return Ok();
	}

	[HttpPost("{id}/remove-admin")]
	public async Task<IActionResult> RemoveAdmin(string id)
	{
		var user = await _userManager.FindByIdAsync(id);

		if (user == null)
			return NotFound();

		await _userManager.RemoveFromRoleAsync(user, "Admin");

		return Ok();
	}

	[HttpPost("{id}/block")]
	public async Task<IActionResult> BlockUser(string id)
	{
		var user = await _userManager.FindByIdAsync(id);

		if (user == null)
			return NotFound();

		user.IsBlocked = true;
		await _userManager.UpdateAsync(user);

		return Ok();
	}

	[HttpPost("{id}/unblock")]
	public async Task<IActionResult> UnblockUser(string id)
	{
		var user = await _userManager.FindByIdAsync(id);

		if (user == null)
			return NotFound();

		user.IsBlocked = false;
		await _userManager.UpdateAsync(user);

		return Ok();
	}
}