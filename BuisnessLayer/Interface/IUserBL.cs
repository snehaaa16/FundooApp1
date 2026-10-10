using System;
using ModelLayer;
using ModelLayer.Models;

namespace BuisnessLayer.Interface
{
	public interface IUserBL
	{
        public Task<RegistrationModel> RegisterUserBL(RegistrationModel registrationModel);
        public Task<UserResponseModel> LoginUserBL(LoginModel model);
    }
}

