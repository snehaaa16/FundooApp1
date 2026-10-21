using System;
using ModelLayer;
using ModelLayer.Models;

namespace RepositoryLayer.Interface
{
	public interface IUserRL
	{
        public Task<RegistrationModel> RegisterUserRL(RegistrationModel registrationModel);
        public Task<UserResponseModel> LoginUser(LoginModel loginuser);
        public Task<ForgotPasswordResponseModel> ForgotPasswordAsyncRL(ForgotPasswordRequestModel req);
        public Task<ForgotPasswordResponseModel> SaveTokenRL(PasswordResetTokenModel tokenModel);
        public Task<ResetPasswordResponseModel> ResetPasswordAsyncRL(ResetPasswordRequestModel req);
    }
}