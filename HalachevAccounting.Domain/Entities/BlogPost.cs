namespace HalachevAccounting.Domain.Entities
{
	public class BlogPost
	{
		public int Id { get; set; }

		public string Title { get; set; } = string.Empty;

		public string Excerpt { get; set; } = string.Empty;

		public string Content { get; set; } = string.Empty;

		public bool IsPublished { get; set; }

		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
	}
}