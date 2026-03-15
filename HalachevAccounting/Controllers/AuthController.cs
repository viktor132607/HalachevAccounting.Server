using HalachevAccounting.Application.Interfaces;
using HalachevAccounting.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HalachevAccounting.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
	private readonly UserManager<ApplicationUser> _userManager;
	private readonly SignInManager<ApplicationUser> _signInManager;
	private readonly IEmailService _emailService;
	private readonly IConfiguration _configuration;

	public AuthController(
		UserManager<ApplicationUser> userManager,
		SignInManager<ApplicationUser> signInManager,
		IEmailService emailService,
		IConfiguration configuration)
	{
		_userManager = userManager;
		_signInManager = signInManager;
		_emailService = emailService;
		_configuration = configuration;
	}

	[HttpPost("register")]
	public async Task<IActionResult> Register([FromBody] RegisterRequest model)
	{
		if (string.IsNullOrWhiteSpace(model.Email) ||
			string.IsNullOrWhiteSpace(model.Password) ||
			string.IsNullOrWhiteSpace(model.ConfirmPassword))
		{
			return BadRequest(new { message = "Email, password and confirm password are required." });
		}

		if (model.Password != model.ConfirmPassword)
			return BadRequest(new { message = "Passwords do not match." });

		var existingUser = await _userManager.FindByEmailAsync(model.Email);
		if (existingUser != null)
			return BadRequest(new { message = "User with this email already exists." });

		var user = new ApplicationUser
		{
			UserName = model.Email,
			Email = model.Email,
			EmailConfirmed = false
		};

		var result = await _userManager.CreateAsync(user, model.Password);

		if (!result.Succeeded)
		{
			return BadRequest(new
			{
				message = "Registration failed.",
				errors = result.Errors.Select(e => e.Description)
			});
		}

		await _userManager.AddToRoleAsync(user, "User");

		try
		{
			var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
			var apiUrl = _configuration["Api:Url"] ?? "https://localhost:7144";

			var confirmUrl =
				$"{apiUrl}/api/auth/confirm-email?email={Uri.EscapeDataString(user.Email!)}&token={Uri.EscapeDataString(token)}";

			await _emailService.SendAsync(
				user.Email!,
				"Confirm your account",
				$"""
                <p>Welcome to Halachev Accounting.</p>
                <p>Please confirm your email by clicking the link below:</p>
                <p><a href="{confirmUrl}">Confirm email</a></p>
                """
			);
		}
		catch
		{
			return Ok(new
			{
				message = "Registration successful, but confirmation email could not be sent."
			});
		}

		return Ok(new
		{
			message = "Registration successful. Please check your email to confirm your account."
		});
	}

	[HttpGet("confirm-email")]
	public async Task<IActionResult> ConfirmEmail([FromQuery] string email, [FromQuery] string token)
	{
		var frontendUrl = _configuration["Frontend:Url"] ?? "http://localhost:5175";

		if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(token))
			return Redirect($"{frontendUrl}/identity/login?confirmed=0");

		var user = await _userManager.FindByEmailAsync(email);
		if (user == null)
			return Redirect($"{frontendUrl}/identity/login?confirmed=0");

		var decodedToken = Uri.UnescapeDataString(token);
		var result = await _userManager.ConfirmEmailAsync(user, decodedToken);

		if (!result.Succeeded)
			return Redirect($"{frontendUrl}/identity/login?confirmed=0");

		return Redirect($"{frontendUrl}/identity/login?confirmed=1");
	}

	[HttpPost("login")]
	public async Task<IActionResult> Login([FromBody] LoginRequest model)
	{
		if (string.IsNullOrWhiteSpace(model.Email) || string.IsNullOrWhiteSpace(model.Password))
			return BadRequest(new { message = "Email and password are required." });

		var user = await _userManager.FindByEmailAsync(model.Email);
		if (user == null)
			return Unauthorized(new { message = "Invalid email or password." });

		if (user.IsBlocked)
			return Unauthorized(new { message = "Your account is blocked." });

		if (!user.EmailConfirmed)
			return Unauthorized(new { message = "Please confirm your email before logging in." });

		var result = await _signInManager.PasswordSignInAsync(
			user.UserName!,
			model.Password,
			isPersistent: false,
			lockoutOnFailure: false);

		if (!result.Succeeded)
			return Unauthorized(new { message = "Invalid email or password." });

		var roles = await _userManager.GetRolesAsync(user);

		return Ok(new
		{
			message = "Login successful.",
			email = user.Email,
			roles
		});
	}

	[Authorize]
	[HttpPost("logout")]
	public async Task<IActionResult> Logout()
	{
		await _signInManager.SignOutAsync();
		return Ok(new { message = "Logged out successfully." });
	}

	[HttpGet("me")]
	public async Task<IActionResult> Me()
	{
		if (User.Identity?.IsAuthenticated != true)
		{
			return Ok(new
			{
				isAuthenticated = false
			});
		}

		var user = await _userManager.GetUserAsync(User);
		if (user == null)
		{
			return Ok(new
			{
				isAuthenticated = false
			});
		}

		var roles = await _userManager.GetRolesAsync(user);

		return Ok(new
		{
			isAuthenticated = true,
			email = user.Email,
			roles
		});
	}

	[HttpPost("forgot-password")]
	public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest model)
	{
		if (string.IsNullOrWhiteSpace(model.Email))
			return BadRequest(new { message = "Email is required." });

		var user = await _userManager.FindByEmailAsync(model.Email);

		if (user == null || !user.EmailConfirmed)
			return Ok(new { message = "If an account with that email exists, a reset link has been sent." });

		var token = await _userManager.GeneratePasswordResetTokenAsync(user);
		var apiUrl = _configuration["Api:Url"] ?? "https://localhost:7144";

		var resetUrl =
			$"{apiUrl}/api/auth/reset-password?email={Uri.EscapeDataString(user.Email!)}&token={Uri.EscapeDataString(token)}";

		await _emailService.SendAsync(
			user.Email!,
			"Reset your password",
			$"""
            <p>You requested a password reset.</p>
            <p>Click the link below to set a new password:</p>
            <p><a href="{resetUrl}">Reset password</a></p>
            """
		);

		return Ok(new { message = "If an account with that email exists, a reset link has been sent." });
	}

	[HttpGet("reset-password")]
	public IActionResult ResetPasswordPage([FromQuery] string email, [FromQuery] string token)
	{
		var frontendUrl = _configuration["Frontend:Url"] ?? "http://localhost:5175";

		if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(token))
			return Redirect($"{frontendUrl}/identity/login");

		return Redirect(
			$"{frontendUrl}/identity/reset-password?email={Uri.EscapeDataString(email)}&token={Uri.EscapeDataString(token)}");
	}

	[HttpPost("reset-password")]
	public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest model)
	{
		if (string.IsNullOrWhiteSpace(model.Email) ||
			string.IsNullOrWhiteSpace(model.Token) ||
			string.IsNullOrWhiteSpace(model.Password) ||
			string.IsNullOrWhiteSpace(model.ConfirmPassword))
		{
			return BadRequest(new { message = "Invalid reset request." });
		}

		if (model.Password != model.ConfirmPassword)
			return BadRequest(new { message = "Passwords do not match." });

		var user = await _userManager.FindByEmailAsync(model.Email);
		if (user == null)
			return BadRequest(new { message = "Invalid reset request." });

		var decodedToken = Uri.UnescapeDataString(model.Token);
		var result = await _userManager.ResetPasswordAsync(user, decodedToken, model.Password);

		if (!result.Succeeded)
		{
			return BadRequest(new
			{
				message = "Password reset failed.",
				errors = result.Errors.Select(e => e.Description)
			});
		}

		return Ok(new { message = "Password reset successful." });
	}

	[Authorize]
	[HttpPost("change-password")]
	public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest model)
	{
		if (string.IsNullOrWhiteSpace(model.CurrentPassword) ||
			string.IsNullOrWhiteSpace(model.NewPassword) ||
			string.IsNullOrWhiteSpace(model.ConfirmPassword))
		{
			return BadRequest(new { message = "Current password, new password and confirm password are required." });
		}

		if (model.NewPassword != model.ConfirmPassword)
			return BadRequest(new { message = "Passwords do not match." });

		var user = await _userManager.GetUserAsync(User);
		if (user == null)
			return Unauthorized(new { message = "User not authenticated." });

		if (user.IsBlocked)
			return Unauthorized(new { message = "Your account is blocked." });

		var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

		if (!result.Succeeded)
		{
			return BadRequest(new
			{
				message = "Password change failed.",
				errors = result.Errors.Select(e => e.Description)
			});
		}

		return Ok(new { message = "Password changed successfully." });
	}
}

public class RegisterRequest
{
	public string Email { get; set; } = string.Empty;
	public string Password { get; set; } = string.Empty;
	public string ConfirmPassword { get; set; } = string.Empty;
}

public class LoginRequest
{
	public string Email { get; set; } = string.Empty;
	public string Password { get; set; } = string.Empty;
}

public class ConfirmEmailRequest
{
	public string Email { get; set; } = string.Empty;
	public string Token { get; set; } = string.Empty;
}

public class ForgotPasswordRequest
{
	public string Email { get; set; } = string.Empty;
}

public class ResetPasswordRequest
{
	public string Email { get; set; } = string.Empty;
	public string Token { get; set; } = string.Empty;
	public string Password { get; set; } = string.Empty;
	public string ConfirmPassword { get; set; } = string.Empty;
}

public class ChangePasswordRequest
{
	public string CurrentPassword { get; set; } = string.Empty;
	public string NewPassword { get; set; } = string.Empty;
	public string ConfirmPassword { get; set; } = string.Empty;
}