using System;
using ModelLayer;
using ModelLayer.Models;

namespace RepositoryLayer.Interface
{
	public interface IUserRL
	{
        public Task<RegistrationModel> RegisterUserRL(RegistrationModel registrationModel);
        public Task<UserResponseModel> LoginUser(LoginModel loginuser);
    }
}