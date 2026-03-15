using HalachevAccounting.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ReparoNow.Domain.Entities;

namespace HalachevAccounting.Infrastructure.Data;

public class AppDbContext : IdentityDbContext<ApplicationUser>
{
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
	{
	}

	public DbSet<Service> Services => Set<Service>();
	public DbSet<ServiceRequest> ServiceRequests => Set<ServiceRequest>();
	public DbSet<BlogPost> BlogPosts => Set<BlogPost>();
	public DbSet<ContactRequest> ContactRequests => Set<ContactRequest>();
	public DbSet<EmailLog> EmailLogs => Set<EmailLog>();

	protected override void OnModelCreating(ModelBuilder builder)
	{
		base.OnModelCreating(builder);

		builder.Entity<ApplicationUser>()
			.Property(x => x.IsBlocked)
			.HasDefaultValue(false);

		builder.Entity<BlogPost>(entity =>
		{
			entity.HasKey(x => x.Id);

			entity.Property(x => x.Title)
				.IsRequired()
				.HasMaxLength(200);

			entity.Property(x => x.Excerpt)
				.HasMaxLength(500);

			entity.Property(x => x.Content)
				.IsRequired();

			entity.Property(x => x.IsPublished)
				.IsRequired();

			entity.Property(x => x.CreatedAt)
				.IsRequired();

			entity.HasIndex(x => x.CreatedAt);
		});

		builder.Entity<ContactRequest>(entity =>
		{
			entity.HasKey(x => x.Id);

			entity.Property(x => x.Name)
				.IsRequired()
				.HasMaxLength(100);

			entity.Property(x => x.Email)
				.IsRequired()
				.HasMaxLength(150);

			entity.Property(x => x.Phone)
				.HasMaxLength(30);

			entity.Property(x => x.Subject)
				.HasMaxLength(200);

			entity.Property(x => x.Message)
				.IsRequired()
				.HasMaxLength(4000);

			entity.Property(x => x.AdminComment)
				.HasMaxLength(2000);

			entity.HasIndex(x => x.CreatedAtUtc);
			entity.HasIndex(x => x.Status);
		});

		builder.Entity<EmailLog>(entity =>
		{
			entity.HasKey(x => x.Id);

			entity.Property(x => x.ToEmail)
				.IsRequired()
				.HasMaxLength(150);

			entity.Property(x => x.Subject)
				.IsRequired()
				.HasMaxLength(200);

			entity.Property(x => x.Body)
				.IsRequired();

			entity.HasOne(x => x.ContactRequest)
				.WithMany()
				.HasForeignKey(x => x.ContactRequestId)
				.OnDelete(DeleteBehavior.SetNull);
		});

		builder.Entity<ServiceRequest>()
			.Property(x => x.OfferedPrice)
			.HasPrecision(18, 2);
	}
}