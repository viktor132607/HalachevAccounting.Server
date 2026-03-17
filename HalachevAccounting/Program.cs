using HalachevAccounting.Application.Interfaces;
using HalachevAccounting.Domain.Entities;
using HalachevAccounting.Infrastructure.Data;
using HalachevAccounting.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
	.AddJsonOptions(options =>
	{
		options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
	});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IEmailService, EmailService>();

builder.Services.AddDbContext<AppDbContext>(options =>
	options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services
	.AddIdentity<ApplicationUser, IdentityRole>(options =>
	{
		options.Password.RequireDigit = true;
		options.Password.RequireLowercase = true;
		options.Password.RequireUppercase = true;
		options.Password.RequireNonAlphanumeric = false;
		options.Password.RequiredLength = 6;

		options.User.RequireUniqueEmail = true;
		options.SignIn.RequireConfirmedEmail = true;
	})
	.AddEntityFrameworkStores<AppDbContext>()
	.AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
	options.Cookie.HttpOnly = true;
	options.Cookie.Name = "HalachevAccounting.Auth";
	options.Cookie.SameSite = SameSiteMode.None;
	options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
	options.LoginPath = "/identity/login";
	options.AccessDeniedPath = "/identity/login";
	options.SlidingExpiration = true;

	options.Events.OnRedirectToLogin = context =>
	{
		if (context.Request.Path.StartsWithSegments("/api"))
		{
			context.Response.StatusCode = StatusCodes.Status401Unauthorized;
			return Task.CompletedTask;
		}

		context.Response.Redirect(context.RedirectUri);
		return Task.CompletedTask;
	};

	options.Events.OnRedirectToAccessDenied = context =>
	{
		if (context.Request.Path.StartsWithSegments("/api"))
		{
			context.Response.StatusCode = StatusCodes.Status403Forbidden;
			return Task.CompletedTask;
		}

		context.Response.Redirect(context.RedirectUri);
		return Task.CompletedTask;
	};
});

builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowFrontend", policy =>
	{
		policy
			.SetIsOriginAllowed(origin =>
			{
				if (!Uri.TryCreate(origin, UriKind.Absolute, out var uri))
					return false;

				return uri.Host.Equals("localhost", StringComparison.OrdinalIgnoreCase)
					|| uri.Host.Equals("127.0.0.1", StringComparison.OrdinalIgnoreCase);
			})
			.AllowAnyHeader()
			.AllowAnyMethod()
			.AllowCredentials();
	});
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
	await AppDataSeeder.SeedAsync(scope.ServiceProvider);
}

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowFrontend");

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/api/services", () =>
{
	return new[] { "Accounting", "Consulting", "Tax" };
});

app.MapControllers();

app.Run();