using System;
using ModelLayer;
using ModelLayer.Models;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;

namespace RepositoryLayer.Service
{
	public class UserRL: IUserRL
    {
        FundooContext fundooContext;

        public UserRL(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }

        private UserResponseModel ToResponse(UserEntity user)
        {
            UserResponseModel response = new UserResponseModel();
            response.UserId = user.UserId;
            response.ContactNo = user.PhoneNumber;
            response.Email = user.Email;
            response.Fname = user.FirstName;
            response.Lname = user.LastName;
            return response;
        }

        public async Task<RegistrationModel> RegisterUserRL(RegistrationModel registrationModel)
        {
            UserEntity userEntity = new UserEntity();
            userEntity.FirstName = registrationModel.FirstName;
            userEntity.LastName = registrationModel.LastName;
            userEntity.Email = registrationModel.Email;
            string hashPass = BCrypt.Net.BCrypt.HashPassword(registrationModel.Password);
            userEntity.Password = hashPass;
            userEntity.PhoneNumber = registrationModel.ContactNo;

            await fundooContext.Users.AddAsync(userEntity);

            await fundooContext.SaveChangesAsync();

            return registrationModel;
        }

        public async Task<UserResponseModel> LoginUser(LoginModel loginuser)
        {
            string email = loginuser.Email;
            UserEntity? user = await fundooContext.Users.FirstOrDefaultAsync(e => e.Email == email);
            if (user == null) return null;

            bool isValid = BCrypt.Net.BCrypt.Verify(loginuser.Password, user.Password);
            if (!isValid) return null;
            return ToResponse(user);
        }
    }
}

