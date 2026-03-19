using HalachevAccounting.Application.DTOs.ContactRequests;
using HalachevAccounting.Application.Interfaces;
using HalachevAccounting.Domain.Entities;
using HalachevAccounting.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReparoNow.Domain.Entities;

namespace HalachevAccounting.Api.Controllers;

[ApiController]
[Route("api/contact-requests")]
public class ContactRequestsController : ControllerBase
{
	private readonly AppDbContext _context;
	private readonly IEmailService _emailService;
	private readonly IConfiguration _configuration;

	public ContactRequestsController(
		AppDbContext context,
		IEmailService emailService,
		IConfiguration configuration)
	{
		_context = context;
		_emailService = emailService;
		_configuration = configuration;
	}

	[HttpPost]
	public async Task<IActionResult> Create([FromBody] CreateContactRequestDto dto)
	{
		var ownerEmail = _configuration["Email:OwnerEmail"];

		if (string.IsNullOrWhiteSpace(ownerEmail))
			return StatusCode(500, new { message = "Owner email is not configured." });

		var entity = new ContactRequest
		{
			Id = Guid.NewGuid(),
			Name = dto.Name,
			Email = dto.Email,
			Phone = dto.Phone,
			Subject = dto.Subject,
			Message = dto.Message,
			CreatedAtUtc = DateTime.UtcNow
		};

		_context.ContactRequests.Add(entity);
		await _context.SaveChangesAsync();

		var body = $@"
<h3>New contact request</h3>
<p><b>Name:</b> {dto.Name}</p>
<p><b>Email:</b> {dto.Email}</p>
<p><b>Phone:</b> {dto.Phone}</p>
<p><b>Subject:</b> {dto.Subject}</p>
<p><b>Message:</b> {dto.Message}</p>
";

		try
		{
			await _emailService.SendAsync(ownerEmail, "New contact request", body);

			_context.EmailLogs.Add(new EmailLog
			{
				Id = Guid.NewGuid(),
				ContactRequestId = entity.Id,
				ToEmail = ownerEmail,
				Subject = "New contact request",
				Body = body,
				IsSent = true,
				SentAtUtc = DateTime.UtcNow,
				CreatedAtUtc = DateTime.UtcNow
			});

			await _context.SaveChangesAsync();

			return Ok(new { message = "Request submitted successfully." });
		}
		catch (Exception ex)
		{
			_context.EmailLogs.Add(new EmailLog
			{
				Id = Guid.NewGuid(),
				ContactRequestId = entity.Id,
				ToEmail = ownerEmail,
				Subject = "New contact request",
				Body = body,
				IsSent = false,
				ErrorMessage = ex.ToString(),
				CreatedAtUtc = DateTime.UtcNow
			});

			await _context.SaveChangesAsync();

			return StatusCode(500, new
			{
				message = "Contact request saved, but email sending failed."
			});
		}
	}

	[HttpGet]
	public async Task<ActionResult<IEnumerable<ContactRequestListDto>>> GetAll()
	{
		var items = await _context.ContactRequests
			.OrderByDescending(x => x.CreatedAtUtc)
			.Select(x => new ContactRequestListDto
			{
				Id = x.Id,
				Name = x.Name,
				Email = x.Email,
				Phone = x.Phone,
				Subject = x.Subject,
				Status = x.Status,
				IsArchived = x.IsArchived,
				CreatedAtUtc = x.CreatedAtUtc
			})
			.ToListAsync();

		return Ok(items);
	}

	[HttpGet("{id:guid}")]
	public async Task<ActionResult<ContactRequest>> GetById(Guid id)
	{
		var entity = await _context.ContactRequests.FirstOrDefaultAsync(x => x.Id == id);

		if (entity == null)
			return NotFound();

		return Ok(entity);
	}

	[HttpPut("{id:guid}")]
	public async Task<IActionResult> Update(Guid id, UpdateContactRequestDto dto)
	{
		var entity = await _context.ContactRequests.FindAsync(id);

		if (entity == null)
			return NotFound();

		entity.Status = dto.Status;
		entity.AdminComment = dto.AdminComment;
		entity.IsArchived = dto.IsArchived;
		entity.UpdatedAtUtc = DateTime.UtcNow;

		await _context.SaveChangesAsync();

		return Ok(new { message = "Request updated successfully." });
	}
}