using System;
using ModelLayer.Models;

namespace BuisnessLayer.Interface
{
	public interface IForgotPasswordBL
    {
		public Task<ForgotPasswordResponseModel> ForgotPasswordAsyncBL(ForgotPasswordRequestModel req);
        public PasswordResetTokenModel GenerateToken(string email);

    }
}

