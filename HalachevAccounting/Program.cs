using System.Security.Claims;
using System.Text.Json.Serialization;
using System.Threading.RateLimiting;
using HalachevAccounting.Api.Infrastructure;
using HalachevAccounting.Api.Middleware;
using HalachevAccounting.Api.Services;
using HalachevAccounting.Application.Interfaces;
using HalachevAccounting.Domain.Entities;
using HalachevAccounting.Infrastructure.Data;
using HalachevAccounting.Infrastructure.Services;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .Enrich.WithProperty("Application", "HalachevAccounting.Api")
    .WriteTo.Console()
    .CreateLogger();

builder.Host.UseSerilog();

string? sentryDsn =
    builder.Configuration["Sentry:Dsn"] ??
    Environment.GetEnvironmentVariable("SENTRY_DSN");

if (!string.IsNullOrWhiteSpace(sentryDsn))
{
    builder.WebHost.UseSentry(options =>
    {
        options.Dsn = sentryDsn;
        options.SendDefaultPii = false;
        options.AttachStacktrace = true;
        options.TracesSampleRate = 0.05;
        options.Environment = builder.Environment.EnvironmentName;
    });
}

string port = Environment.GetEnvironmentVariable("PORT") ?? "10000";
builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.AddMemoryCache();
builder.Services.AddScoped<IEmailService, EmailService>();

string connectionString =
    builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("ConnectionStrings:DefaultConnection is missing.");

builder.Services.AddSingleton(sp =>
    new PostgresAdvisoryLock(
        connectionString,
        sp.GetRequiredService<ILogger<PostgresAdvisoryLock>>()));

builder.Services.AddSingleton(sp =>
    new DatabaseBackupService(
        connectionString,
        sp.GetRequiredService<ILogger<DatabaseBackupService>>(),
        sp.GetRequiredService<PostgresAdvisoryLock>()));

builder.Services.AddScoped<DatabaseSessionContextInterceptor>();

builder.Services.AddDbContext<AppDbContext>((serviceProvider, options) =>
{
    options.UseNpgsql(connectionString);
    options.AddInterceptors(serviceProvider.GetRequiredService<DatabaseSessionContextInterceptor>());
});

builder.Services
    .AddIdentity<ApplicationUser, IdentityRole>(options =>
    {
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequiredLength = 8;
        options.User.RequireUniqueEmail = true;
        options.SignIn.RequireConfirmedEmail = true;

        options.Lockout.AllowedForNewUsers = true;
        options.Lockout.MaxFailedAccessAttempts = 5;
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
    })
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.Name = "HalachevAccounting.Auth";
    options.Cookie.SameSite = SameSiteMode.Lax;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Cookie.IsEssential = true;
    options.LoginPath = "/identity/login";
    options.AccessDeniedPath = "/identity/login";
    options.SlidingExpiration = true;
    options.ExpireTimeSpan = TimeSpan.FromHours(8);

    options.Events.OnRedirectToLogin = context =>
    {
        if (context.Request.Path.StartsWithSegments("/api"))
        {
            return ApiErrorResponseWriter.WriteAsync(
                context.HttpContext,
                StatusCodes.Status401Unauthorized,
                "Unauthorized",
                "Unauthorized.");
        }

        context.Response.Redirect(context.RedirectUri);
        return Task.CompletedTask;
    };

    options.Events.OnRedirectToAccessDenied = context =>
    {
        if (context.Request.Path.StartsWithSegments("/api"))
        {
            return ApiErrorResponseWriter.WriteAsync(
                context.HttpContext,
                StatusCodes.Status403Forbidden,
                "Forbidden",
                "Forbidden.");
        }

        context.Response.Redirect(context.RedirectUri);
        return Task.CompletedTask;
    };
});

builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
    options.KnownIPNetworks.Clear();
    options.KnownProxies.Clear();
});

builder.Services.AddRateLimiter(options =>
{
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
    options.OnRejected = async (context, _) =>
    {
        await ApiErrorResponseWriter.WriteAsync(
            context.HttpContext,
            StatusCodes.Status429TooManyRequests,
            "RateLimitExceeded",
            "Too many requests.");
    };

    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(context =>
        RateLimitPartition.GetFixedWindowLimiter(
            GetPartitionKey(context),
            _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 300,
                Window = TimeSpan.FromMinutes(1),
                QueueLimit = 0,
                AutoReplenishment = true
            }));

    options.AddPolicy("auth", context =>
        RateLimitPartition.GetFixedWindowLimiter(
            GetPartitionKey(context),
            _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 5,
                Window = TimeSpan.FromMinutes(1),
                QueueLimit = 0,
                AutoReplenishment = true
            }));

    options.AddPolicy("contact", context =>
        RateLimitPartition.GetFixedWindowLimiter(
            GetPartitionKey(context),
            _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 3,
                Window = TimeSpan.FromMinutes(5),
                QueueLimit = 0,
                AutoReplenishment = true
            }));
});

string? frontendUrl = builder.Configuration["Frontend:Url"]?.TrimEnd('/');
List<string> allowedOrigins =
[
    "http://localhost:5173",
    "http://localhost:5175",
    "https://nhalachev.com",
    "https://www.nhalachev.com"
];

if (!string.IsNullOrWhiteSpace(frontendUrl) &&
    !allowedOrigins.Contains(frontendUrl, StringComparer.OrdinalIgnoreCase))
{
    allowedOrigins.Add(frontendUrl);
}

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy
            .WithOrigins(allowedOrigins.ToArray())
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

var app = builder.Build();

app.UseForwardedHeaders();
app.UseMiddleware<CorrelationIdMiddleware>();

app.UseSerilogRequestLogging(options =>
{
    options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
    {
        diagnosticContext.Set("TraceId", httpContext.TraceIdentifier);
        diagnosticContext.Set("RequestHost", httpContext.Request.Host.Value);

        string? userId = httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!string.IsNullOrWhiteSpace(userId))
        {
            diagnosticContext.Set("UserId", userId);
        }
    };
});

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
app.UseMiddleware<SecurityHeadersMiddleware>();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

using (IServiceScope scope = app.Services.CreateScope())
{
    AppDbContext db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    const int maxAttempts = 5;
    for (int attempt = 1; attempt <= maxAttempts; attempt++)
    {
        try
        {
            await db.Database.MigrateAsync();
            break;
        }
        catch (Exception exception)
            when (attempt < maxAttempts &&
                  (exception is NpgsqlException || exception is TimeoutException))
        {
            TimeSpan delay = TimeSpan.FromSeconds(Math.Pow(2, attempt));
            app.Logger.LogWarning(
                exception,
                "Transient database startup failure. Attempt {Attempt}/{MaxAttempts}; retrying in {DelaySeconds}s.",
                attempt,
                maxAttempts,
                delay.TotalSeconds);
            await Task.Delay(delay);
        }
    }

    await AppDataSeeder.SeedAsync(scope.ServiceProvider);
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowFrontend");
app.UseAuthentication();
app.UseRateLimiter();
app.UseAuthorization();

app.MapGet("/api/services", () => new[] { "Accounting", "Consulting", "Tax" });
app.MapControllers();

app.Run();

static string GetPartitionKey(HttpContext context) =>
    context.User.FindFirstValue(ClaimTypes.NameIdentifier)
    ?? context.Connection.RemoteIpAddress?.ToString()
    ?? "unknown";
