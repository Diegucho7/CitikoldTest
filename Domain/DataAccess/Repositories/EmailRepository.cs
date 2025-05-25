using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using RetailCitikold.Domain.DataAccess.Intefaces.Repositories;
using RetailCitikold.Domain.Dtos;

namespace RetailCitikold.Domain.DataAccess.Repositories;

public class EmailRepository : IEmailService
{
    private readonly SmtpSettings _smtpSettings;

    public EmailRepository(IOptions<SmtpSettings> smtpOptions)
    {
        _smtpSettings = smtpOptions.Value;
    }
    public async Task SendAsync(string to, string subject, string body)
    {
        using var client = new SmtpClient(_smtpSettings.Host, _smtpSettings.Port);
        client.Credentials = new NetworkCredential(_smtpSettings.Username, _smtpSettings.Password);
        client.EnableSsl = _smtpSettings.EnableSsl;

        var message = new MailMessage
        {
            From = new MailAddress(_smtpSettings.From),
            Subject = subject,
            Body = body,
            IsBodyHtml = true
        };
        message.To.Add(to);

        await client.SendMailAsync(message);
    }
}