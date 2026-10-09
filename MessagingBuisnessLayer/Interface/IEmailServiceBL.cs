using System;
using MessagingModelLayer.Models;

namespace MessagingBuisnessLayer.Interface
{
	public interface IEmailServiceBL
	{
        public Task<EmailResponseModel> SendEmailAsync(EmailRequestModel req);
    }
}

