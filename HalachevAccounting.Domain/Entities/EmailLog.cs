namespace ReparoNow.Domain.Entities;

public class EmailLog
{
	public Guid Id { get; set; }

	public Guid? ContactRequestId { get; set; }
	public ContactRequest? ContactRequest { get; set; }

	public string ToEmail { get; set; } = null!;
	public string Subject { get; set; } = null!;
	public string Body { get; set; } = null!;

	public bool IsSent { get; set; } = false;
	public string? ErrorMessage { get; set; }

	public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
	public DateTime? SentAtUtc { get; set; }
}