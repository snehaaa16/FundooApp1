using System;
using BuisnessLayer.Interface;
using ModelLayer;
using ModelLayer.Models;
using RepositoryLayer.Interface;

namespace BuisnessLayer.Service
{
	public class UserBL: IUserBL
    {
        private IUserRL _userRL;
        private IJwtService _jwtService;
        public UserBL(IUserRL _userRL,IJwtService _jwtService)
        {
            this._userRL = _userRL;
            this._jwtService = _jwtService;
        }

        public RegistrationModel RegisterUserBL(RegistrationModel registrationModel)
        {
            return _userRL.RegisterUserRL(registrationModel);
        }

        public UserResponseModel LoginUserBL(LoginModel loginUser)
        {
            var user = _userRL.LoginUser(loginUser);
            if (user == null) return null;
            user.Token = _jwtService.GenerateToken(user);
            return user;
        }
    }
}

