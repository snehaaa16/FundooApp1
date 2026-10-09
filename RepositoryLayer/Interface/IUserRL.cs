using System;
using ModelLayer;
using ModelLayer.Models;

namespace RepositoryLayer.Interface
{
	public interface IUserRL
	{
        public RegistrationModel RegisterUserRL(RegistrationModel registrationModel);
        public UserResponseModel LoginUser(LoginModel loginuser);
    }
}