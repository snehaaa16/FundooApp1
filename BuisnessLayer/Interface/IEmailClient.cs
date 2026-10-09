using ModelLayer.Models;
namespace BuisnessLayer.Interface
{
	public interface IEmailClient
	{
		public Task<EmailResponseModel> SendEmailAsync(EmailRequestModel req);
	}
}

