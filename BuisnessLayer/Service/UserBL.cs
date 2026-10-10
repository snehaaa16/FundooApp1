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

        public async Task<RegistrationModel> RegisterUserBL(RegistrationModel registrationModel)
        {
            return await _userRL.RegisterUserRL(registrationModel);
        }

        public async Task<UserResponseModel> LoginUserBL(LoginModel loginUser)
        {
            var user = await _userRL.LoginUser(loginUser);
            if (user == null) return null;
            user.Token = _jwtService.GenerateToken(user);
            return user;
        }
    }
}

