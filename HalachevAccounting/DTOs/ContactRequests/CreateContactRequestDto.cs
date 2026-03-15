namespace HalachevAccounting.Application.DTOs.ContactRequests;

public class CreateContactRequestDto
{
	public string Name { get; set; } = null!;
	public string Email { get; set; } = null!;
	public string? Phone { get; set; }
	public string? Subject { get; set; }
	public string Message { get; set; } = null!;
}