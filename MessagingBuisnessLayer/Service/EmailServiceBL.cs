using System.Net;
using System.Net.Mail;
using MessagingBuisnessLayer.Interface;
using MessagingModelLayer.Models;
using Microsoft.Extensions.Options;

namespace MessagingBuisnessLayer.Service
{
	public class EmailServiceBL: IEmailServiceBL
    {
        private readonly SmtpSettings _smtpSettings;

		public EmailServiceBL(IOptions<SmtpSettings> _smtpSettings)
		{
            this._smtpSettings = _smtpSettings.Value;
		}

        public async Task<EmailResponseModel> SendEmailAsync(EmailRequestModel request)
        {
            try
            {
                using var client = new SmtpClient(_smtpSettings.Host, _smtpSettings.Port);

                client.Credentials = new NetworkCredential(
                    _smtpSettings.Username,
                    _smtpSettings.Password);

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
            catch (Exception ex)
            {
                return new EmailResponseModel
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }
    }
}

