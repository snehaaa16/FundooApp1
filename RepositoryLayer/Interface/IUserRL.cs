using System;
using ModelLayer;
using ModelLayer.Models;
using RepositoryLayer.Entity;

namespace RepositoryLayer.Interface
{
	public interface IUserRL
	{
        public Task<UserEntity> RegisterUserRL(UserEntity userEntity);
        public Task<UserEntity> LoginUser(LoginModel loginuser);
        public Task<ForgotPasswordResponseModel> ForgotPasswordAsyncRL(ForgotPasswordRequestModel req);
        public Task<ResetPasswordResponseModel> ResetPasswordAsyncRL(ResetPasswordRequestModel req);
    }
}