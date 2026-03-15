using ReparoNow.Domain.Enums;

namespace HalachevAccounting.Application.DTOs.ContactRequests;

public class ContactRequestListDto
{
	public Guid Id { get; set; }

	public string Name { get; set; } = null!;
	public string Email { get; set; } = null!;
	public string? Phone { get; set; }
	public string? Subject { get; set; }

	public ContactRequestStatus Status { get; set; }
	public bool IsArchived { get; set; }

	public DateTime CreatedAtUtc { get; set; }
}