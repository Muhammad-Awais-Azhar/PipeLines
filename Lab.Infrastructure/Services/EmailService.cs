using Microsoft.Extensions.Options;
using SVX.Application.Interfaces;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace SVX.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly SmtpClient _smtpClient;
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;

            _smtpClient = new SmtpClient(_emailSettings.Host, _emailSettings.Port)
            {
                Credentials = new NetworkCredential(_emailSettings.Username, _emailSettings.Password),
                EnableSsl = true
            };
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            using (var message = new MailMessage(_emailSettings.SenderEmail, to)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
            {
                await _smtpClient.SendMailAsync(message);
            }
        }
    }
}
