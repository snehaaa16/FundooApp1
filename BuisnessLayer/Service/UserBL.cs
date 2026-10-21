using System;
using System.Security.Cryptography;
using BuisnessLayer.Interface;
using Middleware.Service.EmailService;
using Middleware.Service.JwtService;
using ModelLayer;
using ModelLayer.Models;
using RepositoryLayer.Interface;
using RepositoryLayer.Service;

namespace BuisnessLayer.Service
{
	public class UserBL: IUserBL
    {
        private IUserRL _userRL;
        private IJwtService _jwtService;
        private IEmailService _emailService;

        public UserBL(IUserRL _userRL, IJwtService _jwtService, IEmailService _emailService)
        {
            this._userRL = _userRL;
            this._jwtService = _jwtService;
            this._emailService = _emailService;
        }

        public async Task<RegistrationModel> RegisterUserBL(RegistrationModel registrationModel)
        {
            return await _userRL.RegisterUserRL(registrationModel);
        }

        public async Task<UserResponseModel?> LoginUserBL(LoginModel loginUser)
        {
            var user = await _userRL.LoginUser(loginUser);
            if (user == null) return null;
            user.Token = _jwtService.GenerateToken(user);
            return user;
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

            EmailResponseModel emailResponse = await _emailService.SendEmailAsync(emailRequest);

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
            var res = await _userRL.ForgotPasswordAsyncRL(req);

            if (!res.Scuccess)
            {
                return res;
            }
            PasswordResetTokenModel tokenModel = GenerateToken(req.Email);

            ForgotPasswordResponseModel res2 = await _userRL.SaveTokenRL(tokenModel);

            if (!res2.Scuccess)
            {
                return res2;
            }

            return await SendEmailRequest(tokenModel);
        }

        public async Task<ResetPasswordResponseModel> ResetPasswordAsyncBL(ResetPasswordRequestModel req)
        {
            return await _userRL.ResetPasswordAsyncRL(req);
        }
    }
}

