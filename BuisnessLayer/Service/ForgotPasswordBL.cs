using BuisnessLayer.Interface;
using ModelLayer.Models;
using RepositoryLayer.Interface;
using RepositoryLayer.Service;
using System.Security.Cryptography;

namespace BuisnessLayer.Service
{
	public class ForgotPasswordBL: IForgotPasswordBL
    {
        private IForgotPasswordRL _forgotPasswordRL;
        private IEmailClient _emailClient;

        public ForgotPasswordBL(IForgotPasswordRL _forgotPasswordRL,IEmailClient _emailClient)
        {
            this._forgotPasswordRL = _forgotPasswordRL;
            this._emailClient = _emailClient;
        }

        public PasswordResetTokenModel GenerateToken(string email)
        {
            byte[] tokenBytes = RandomNumberGenerator.GetBytes(32);
            string token = Convert.ToBase64String(tokenBytes);

            PasswordResetTokenModel tokenModel = new PasswordResetTokenModel();
            tokenModel.Email = email;
            tokenModel.IsUsed = false;
            tokenModel.ExpiryTime = DateTime.UtcNow.AddMinutes(10);
            tokenModel.Token = token;
            return tokenModel;
        }

        public async Task<ForgotPasswordResponseModel> SendEmailRequest(PasswordResetTokenModel tokenModel)
        {
            EmailRequestModel emailRequest = new EmailRequestModel
            {
                To = tokenModel.Email,
                Subject = "Password Reset",
                Body = $"Your password reset token is: {tokenModel.Token}",
                IsHtml = false
            };

            EmailResponseModel emailResponse= await _emailClient.SendEmailAsync(emailRequest);

            if (!emailResponse.Success)
            {
                return new ForgotPasswordResponseModel
                {
                    Scuccess = false,
                    Message = "Password reset token was generated, but email could not be sent."
                };
            }

            return new ForgotPasswordResponseModel
            {
                Scuccess = true,
                Message = "Password reset email sent successfully."
            };
        }

        public async Task<ForgotPasswordResponseModel> ForgotPasswordAsyncBL(ForgotPasswordRequestModel req)
        {
            var res = await _forgotPasswordRL.ForgotPasswordAsyncRL(req);

            if (!res.Scuccess)
            {
                return res;
            }

            PasswordResetTokenModel tokenModel = GenerateToken(req.Email);

            ForgotPasswordResponseModel res2 = await _forgotPasswordRL.SaveTokenRL(tokenModel);

            if (!res2.Scuccess)
            {
                return res2;
            }

            return await SendEmailRequest(tokenModel);
        }
    }
}