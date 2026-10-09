using System;
using ModelLayer;
using ModelLayer.Models;

namespace BuisnessLayer.Interface
{
	public interface IUserBL
	{
        public RegistrationModel RegisterUserBL(RegistrationModel registrationModel);
        public UserResponseModel LoginUserBL(LoginModel model);
    }
}

