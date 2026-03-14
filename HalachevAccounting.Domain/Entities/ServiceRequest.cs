namespace HalachevAccounting.Domain.Entities;

public class ServiceRequest
{
	public int Id { get; set; }
	public string FullName { get; set; } = null!;
	public string Email { get; set; } = null!;
	public string? PhoneNumber { get; set; }
	public string Message { get; set; } = null!;
	public int? ServiceId { get; set; }
	public Service? Service { get; set; }
	public string Status { get; set; } = "New";
	public string? AdminComment { get; set; }
	public decimal? OfferedPrice { get; set; }
	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
