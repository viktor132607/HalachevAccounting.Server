using HalachevAccounting.Domain.Entities;
using HalachevAccounting.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HalachevAccounting.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BlogController : ControllerBase
{
	private readonly AppDbContext _context;

	public BlogController(AppDbContext context)
	{
		_context = context;
	}

	[HttpGet]
	public async Task<IActionResult> GetPosts()
	{
		var posts = await _context.BlogPosts
			.Where(p => p.IsPublished)
			.OrderByDescending(p => p.CreatedAt)
			.ToListAsync();

		return Ok(posts);
	}

	[HttpGet("{id}")]
	public async Task<IActionResult> GetPost(int id)
	{
		var post = await _context.BlogPosts
			.FirstOrDefaultAsync(p => p.Id == id);

		if (post == null)
			return NotFound();

		return Ok(post);
	}

	[Authorize(Roles = "Admin")]
	[HttpPost]
	public async Task<IActionResult> CreatePost([FromBody] BlogPost post)
	{
		post.CreatedAt = DateTime.UtcNow;

		_context.BlogPosts.Add(post);
		await _context.SaveChangesAsync();

		return Ok(post);
	}

	[Authorize(Roles = "Admin")]
	[HttpPut("{id}")]
	public async Task<IActionResult> UpdatePost(int id, [FromBody] BlogPost updated)
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

	[Authorize(Roles = "Admin")]
	[HttpDelete("{id}")]
	public async Task<IActionResult> DeletePost(int id)
	{
		var post = await _context.BlogPosts.FindAsync(id);

		if (post == null)
			return NotFound();

		_context.BlogPosts.Remove(post);
		await _context.SaveChangesAsync();

		return Ok();
	}
}