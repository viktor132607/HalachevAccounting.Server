using HalachevAccounting.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReparoNow.Domain.Entities;

namespace HalachevAccounting.Infrastructure.Data;

public static class AppDataSeeder
{
	public static async Task SeedAsync(IServiceProvider services)
	{
		using var scope = services.CreateScope();

		var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
		var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
		var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();

		await SeedRoles(roleManager);
		await SeedAdmins(userManager, configuration);
		await SeedTestUsers(userManager, configuration);
		await SeedServiceRequests(services);
	}

	private static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
	{
		string[] roles = { "Admin", "User" };

		foreach (var role in roles)
		{
			if (!await roleManager.RoleExistsAsync(role))
			{
				await roleManager.CreateAsync(new IdentityRole(role));
			}
		}
	}

	private static async Task SeedAdmins(UserManager<ApplicationUser> userManager, IConfiguration configuration)
	{
		var adminPassword = configuration["Seed:AdminPassword"] ?? "Admin123!";

		string[] adminEmails =
		{
			"iliev132607@gmail.com",
			"nthalachev@gmail.com",
			"nikolahalachev2811@gmail.com"
		};

		foreach (var email in adminEmails)
		{
			var user = await userManager.FindByEmailAsync(email);

			if (user == null)
			{
				user = new ApplicationUser
				{
					UserName = email,
					Email = email,
					EmailConfirmed = true,
					IsBlocked = false
				};

				var createResult = await userManager.CreateAsync(user, adminPassword);

				if (!createResult.Succeeded)
					continue;
			}
			else
			{
				user.EmailConfirmed = true;
				user.IsBlocked = false;
				await userManager.UpdateAsync(user);

				var hasPassword = await userManager.HasPasswordAsync(user);

				if (hasPassword)
				{
					var resetToken = await userManager.GeneratePasswordResetTokenAsync(user);
					await userManager.ResetPasswordAsync(user, resetToken, adminPassword);
				}
				else
				{
					await userManager.AddPasswordAsync(user, adminPassword);
				}
			}

			if (!await userManager.IsInRoleAsync(user, "Admin"))
			{
				await userManager.AddToRoleAsync(user, "Admin");
			}

			if (await userManager.IsInRoleAsync(user, "User"))
			{
				await userManager.RemoveFromRoleAsync(user, "User");
			}
		}
	}

	private static async Task SeedTestUsers(UserManager<ApplicationUser> userManager, IConfiguration configuration)
	{
		var testUserPassword = configuration["Seed:TestUserPassword"] ?? "User123!";

		string[] testUserEmails =
		{
			"user1@test.com",
			"user2@test.com",
			"user3@test.com",
			"user4@test.com",
			"user5@test.com"
		};

		foreach (var email in testUserEmails)
		{
			var user = await userManager.FindByEmailAsync(email);

			if (user == null)
			{
				user = new ApplicationUser
				{
					UserName = email,
					Email = email,
					EmailConfirmed = true,
					IsBlocked = false
				};

				var createResult = await userManager.CreateAsync(user, testUserPassword);

				if (!createResult.Succeeded)
					continue;
			}
			else
			{
				user.EmailConfirmed = true;
				user.IsBlocked = false;
				await userManager.UpdateAsync(user);

				var hasPassword = await userManager.HasPasswordAsync(user);

				if (hasPassword)
				{
					var resetToken = await userManager.GeneratePasswordResetTokenAsync(user);
					await userManager.ResetPasswordAsync(user, resetToken, testUserPassword);
				}
				else
				{
					await userManager.AddPasswordAsync(user, testUserPassword);
				}
			}

			if (!await userManager.IsInRoleAsync(user, "User"))
			{
				await userManager.AddToRoleAsync(user, "User");
			}
		}
	}

	private static async Task SeedServiceRequests(IServiceProvider services)
	{
		using var scope = services.CreateScope();
		var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

		if (await context.ServiceRequests.AnyAsync())
			return;

		var requests = new List<ServiceRequest>
		{
			new ServiceRequest
			{
				FullName = "Ivan Petrov",
				Email = "ivan@test.com",
				PhoneNumber = "0888123456",
				Message = "Need monthly accounting service for a small company.",
				OfferedPrice = 120,
				Status = "New"
			},
			new ServiceRequest
			{
				FullName = "Maria Georgieva",
				Email = "maria@test.com",
				PhoneNumber = "0899123456",
				Message = "Need VAT registration consultation.",
				OfferedPrice = 200,
				Status = "New"
			},
			new ServiceRequest
			{
				FullName = "Dimitar Ivanov",
				Email = "dimitar@test.com",
				PhoneNumber = "0877123456",
				Message = "Need help with annual financial statements.",
				OfferedPrice = 150,
				Status = "New"
			},
			new ServiceRequest
			{
				FullName = "Petya Koleva",
				Email = "petya@test.com",
				PhoneNumber = "0887123456",
				Message = "Need payroll and HR administration service.",
				OfferedPrice = 180,
				Status = "New"
			},
			new ServiceRequest
			{
				FullName = "Georgi Dimitrov",
				Email = "georgi@test.com",
				PhoneNumber = "0898123456",
				Message = "Need company registration and accounting setup.",
				OfferedPrice = 300,
				Status = "New"
			}
		};

		context.ServiceRequests.AddRange(requests);
		await context.SaveChangesAsync();
	}
}