using Contracts.Services;
using Infrastructure.Configurations;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using MimeKit;
using Shared.Services.Email;

namespace Infrastructure.Services
{
    public class SmtpEmailService : ISmtpEmailService
    {
        private readonly ILogger<SmtpEmailService> _logger;
        private readonly SMTPEmailSetting _emailSetting;
        private readonly SmtpClient _smtpClient;

        public SmtpEmailService(ILogger<SmtpEmailService> logger, SMTPEmailSetting emailSetting) 
        {
            _logger = logger;
            _emailSetting = emailSetting;
            _smtpClient = new SmtpClient();
        }

        public async Task SendEmailAsync(MailRequest request, CancellationToken cancellationToken = default)
        {
            var emailMessage = new MimeMessage()
            {
                Sender = new MailboxAddress(_emailSetting.SenderName, request.FromEmail ?? _emailSetting.SenderEmail),
                Subject = request.Subject,
                Body = new BodyBuilder
                {
                    HtmlBody = request.Body
                }.ToMessageBody()
            };

            if (request.ToEmails.Any())
            {
                foreach (var toEmail in request.ToEmails)
                {
                    emailMessage.To.Add(MailboxAddress.Parse(toEmail));
                }
            }
            else
            {
                emailMessage.To.Add(MailboxAddress.Parse(request.ToEmail));
            }

            try
            {
                await _smtpClient.ConnectAsync(_emailSetting.Host, _emailSetting.Port, _emailSetting.EnableSSL, cancellationToken);
                await _smtpClient.AuthenticateAsync(_emailSetting.Username, _emailSetting.Password, cancellationToken);
                await _smtpClient.SendAsync(emailMessage, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while sending email");
            }
            finally
            {
                await _smtpClient.DisconnectAsync(true, cancellationToken);
                _smtpClient.Dispose();
            }
        }
    }
}
