using HalachevAccounting.Domain.Entities;
using HalachevAccounting.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HalachevAccounting.Controllers;

[Authorize(Roles = "Admin")]
[ApiController]
[Route("api/admin/blog")]
public class AdminBlogController : ControllerBase
{
	private readonly AppDbContext _context;

	public AdminBlogController(AppDbContext context)
	{
		_context = context;
	}

	[HttpGet]
	public async Task<IActionResult> GetAll()
	{
		var posts = await _context.BlogPosts
			.OrderByDescending(x => x.CreatedAt)
			.ToListAsync();

		return Ok(posts);
	}

	[HttpPost]
	public async Task<IActionResult> Create([FromBody] BlogPost post)
	{
		post.CreatedAt = DateTime.UtcNow;

		_context.BlogPosts.Add(post);
		await _context.SaveChangesAsync();

		return Ok(post);
	}

	[HttpPut("{id}")]
	public async Task<IActionResult> Update(int id, [FromBody] BlogPost updated)
	{
		var post = await _context.BlogPosts.FindAsync(id);

		if (post == null)
			return NotFound();

		post.Title = updated.Title;
		post.Excerpt = updated.Excerpt;
		post.Content = updated.Content;
		post.IsPublished = updated.IsPublished;

		await _context.SaveChangesAsync();

		return Ok(post);
	}

	[HttpDelete("{id}")]
	public async Task<IActionResult> Delete(int id)
	{
		var post = await _context.BlogPosts.FindAsync(id);

		if (post == null)
			return NotFound();

		_context.BlogPosts.Remove(post);
		await _context.SaveChangesAsync();

		return Ok();
	}
}