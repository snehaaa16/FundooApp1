using System;
using ModelLayer;
using ModelLayer.Models;
using RepositoryLayer.Entity;

namespace BuisnessLayer.Interface
{
	public interface IUserBL
	{
        public Task<UserEntity> RegisterUserBL(UserEntity entity);
        public Task<UserEntity?> LoginUserBL(LoginModel loginUser);
    }
}

