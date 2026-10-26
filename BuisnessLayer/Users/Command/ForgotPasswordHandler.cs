using System;
using BuisnessLayer.Interface;
using MediatR;
using Middleware.Service.EmailService;
using Middleware.Service.JwtService;
using ModelLayer.Models;
using RepositoryLayer.Interface;

namespace BuisnessLayer.Users.Command
{
    public class ForgotPasswordHandler
    : IRequestHandler<ForgotPasswordCommand, ForgotPasswordResponseModel>
    {
        private readonly IUserRL _userRL;
        private readonly IEmailService _emailService;
        private readonly IJwtService _jwtService;

        public ForgotPasswordHandler(IUserRL userRL,IEmailService emailService,IJwtService jwtService)
        {
            _userRL = userRL;
            _emailService = emailService;
            _jwtService = jwtService;
        }

        public async Task<ForgotPasswordResponseModel> Handle(ForgotPasswordCommand command,CancellationToken cancellationToken)
        {
            var request = command.Request;

            var result = await _userRL.ForgotPasswordAsyncRL(request);

            if (!result.Scuccess)
                return result;

            var token = _jwtService.GeneratePasswordResetToken(request.Email);

            var emailRequest = new EmailRequestModel
            {
                To = request.Email,
                Subject = "Password Reset",
                Body = $"Your password reset token is: {token}",
                IsHtml = false
            };

            var emailResponse = await _emailService.SendEmailAsync(emailRequest);

            if (!emailResponse.Success)
            {
                return new ForgotPasswordResponseModel
                {
                    Scuccess = false,
                    Message = "Password reset email could not be sent."
                };
            }

            return new ForgotPasswordResponseModel
            {
                Scuccess = true,
                Message = "Password reset email sent successfully."
            };
        }
    }
}

