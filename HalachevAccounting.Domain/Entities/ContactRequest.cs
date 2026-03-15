using ReparoNow.Domain.Enums;

namespace ReparoNow.Domain.Entities;

public class ContactRequest
{
	public Guid Id { get; set; }

	public string Name { get; set; } = null!;
	public string Email { get; set; } = null!;
	public string? Phone { get; set; }
	public string? Subject { get; set; }
	public string Message { get; set; } = null!;

	public ContactRequestStatus Status { get; set; } = ContactRequestStatus.New;
	public string? AdminComment { get; set; }
	public bool IsArchived { get; set; } = false;

	public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
	public DateTime? UpdatedAtUtc { get; set; }
}