using HalachevAccounting.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HalachevAccounting.Infrastructure.Data;

public class AppDbContext : IdentityDbContext<ApplicationUser>
{
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
	{
	}

	public DbSet<Service> Services => Set<Service>();
	public DbSet<ServiceRequest> ServiceRequests => Set<ServiceRequest>();
	public DbSet<BlogPost> BlogPosts => Set<BlogPost>();
}