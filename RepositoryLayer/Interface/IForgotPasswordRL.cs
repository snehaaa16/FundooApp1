using System;
using ModelLayer.Models;

namespace RepositoryLayer.Interface
{
	public interface IForgotPasswordRL
	{
        public Task<ForgotPasswordResponseModel> ForgotPasswordAsyncRL(ForgotPasswordRequestModel req);
        public Task<ForgotPasswordResponseModel> SaveTokenRL(PasswordResetTokenModel tokenModel);
    }
}

