using System;
using ModelLayer.Models;
namespace Middleware.Service.EmailService
{
	public interface IEmailService
	{
        public Task<EmailResponseModel> SendEmailAsync(EmailRequestModel request);

    }
}

