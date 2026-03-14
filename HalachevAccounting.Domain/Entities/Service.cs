namespace HalachevAccounting.Domain.Entities;

public class Service
{
	public int Id { get; set; }
	public string Title { get; set; } = null!;
	public string Description { get; set; } = null!;
	public bool IsActive { get; set; } = true;
}
