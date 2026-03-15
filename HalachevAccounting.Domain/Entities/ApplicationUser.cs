using Microsoft.AspNetCore.Identity;

namespace HalachevAccounting.Domain.Entities;

public class ApplicationUser : IdentityUser
{
	public bool IsBlocked { get; set; } = false;
}