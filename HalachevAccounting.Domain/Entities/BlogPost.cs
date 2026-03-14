namespace HalachevAccounting.Domain.Entities;

public class BlogPost
{
	public int Id { get; set; }
	public string Title { get; set; } = null!;
	public string Slug { get; set; } = null!;
	public string Content { get; set; } = null!;
	public bool IsPublished { get; set; } = false;
	public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
