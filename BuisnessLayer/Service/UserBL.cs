using System;
using System.Security.Cryptography;
using BuisnessLayer.Interface;
using BuisnessLayer.Users.Command;
using Middleware.Service.EmailService;
using Middleware.Service.JwtService;
using ModelLayer;
using ModelLayer.Models;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using RepositoryLayer.Service;

namespace BuisnessLayer.Service
{
	public class UserBL: IUserBL
    {
        private IUserRL _userRL;

        public UserBL(IUserRL _userRL)
        {
            this._userRL = _userRL;
        }

        public async Task<UserEntity> RegisterUserBL(UserEntity entity)
        {
            return await _userRL.RegisterUserRL(entity);
        }

        public async Task<UserEntity?> LoginUserBL(LoginModel loginUser)
        {
            return await _userRL.LoginUser(loginUser);
        }
    }
}

