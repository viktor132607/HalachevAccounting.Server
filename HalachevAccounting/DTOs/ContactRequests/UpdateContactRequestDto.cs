using ReparoNow.Domain.Enums;

namespace HalachevAccounting.Application.DTOs.ContactRequests;

public class UpdateContactRequestDto
{
	public ContactRequestStatus Status { get; set; }
	public string? AdminComment { get; set; }
	public bool IsArchived { get; set; }
}