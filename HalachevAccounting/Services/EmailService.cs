using System.Net;
using System.Net.Mail;
using HalachevAccounting.Application.Interfaces;
using Microsoft.Extensions.Configuration;

namespace HalachevAccounting.Infrastructure.Services;

public class EmailService : IEmailService
{
	private readonly IConfiguration _configuration;

	public EmailService(IConfiguration configuration)
	{
		_configuration = configuration;
	}

	public async Task SendAsync(string toEmail, string subject, string body)
	{
		var smtpHost = _configuration["Email:SmtpHost"];
		var smtpPort = int.Parse(_configuration["Email:SmtpPort"]!);
		var username = _configuration["Email:Username"];
		var password = _configuration["Email:Password"];
		var fromEmail = _configuration["Email:FromEmail"];
		var fromName = _configuration["Email:FromName"];

		var client = new SmtpClient(smtpHost, smtpPort)
		{
			Credentials = new NetworkCredential(username, password),
			EnableSsl = true
		};

		var mail = new MailMessage
		{
			From = new MailAddress(fromEmail!, fromName),
			Subject = subject,
			Body = body,
			IsBodyHtml = true
		};

		mail.To.Add(toEmail);

		await client.SendMailAsync(mail);
	}
}