using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using ModelLayer.Models;

namespace Middleware.Service.EmailService
{
	public class EmailService:IEmailService
	{
        private readonly SmtpSettings _smtpSettings;

        public EmailService(IOptions<SmtpSettings> _smtpSettings)
        {
            this._smtpSettings = _smtpSettings.Value;   
        }

        public async Task<EmailResponseModel> SendEmailAsync(EmailRequestModel request)
        {
            using var client = new SmtpClient(_smtpSettings.Host, _smtpSettings.Port);

            client.Credentials = new NetworkCredential(_smtpSettings.Username,_smtpSettings.Password);

            client.EnableSsl = _smtpSettings.EnableSsl;

            var mail = new MailMessage(
                _smtpSettings.Username,
                request.To,
                request.Subject,
                request.Body);

            mail.IsBodyHtml = request.IsHtml;

            await client.SendMailAsync(mail);

            return new EmailResponseModel
            {
                Success = true,
                Message = "Email sent successfully"
            };

        }
    }
}

