using System.Net.Http.Json;
using BuisnessLayer.Interface;
using ModelLayer.Models;
namespace BuisnessLayer.Service
{
	public class EmailClient: IEmailClient
    {
        private readonly HttpClient _httpClient;

        public EmailClient(HttpClient _httpClient)
		{
            this._httpClient = _httpClient;
		}

        public async Task<EmailResponseModel> SendEmailAsync(EmailRequestModel req)
		{
            var response = await _httpClient.PostAsJsonAsync("api/Messaging/send", req);

            return await response.Content.ReadFromJsonAsync<EmailResponseModel>();
        }
    }
}

