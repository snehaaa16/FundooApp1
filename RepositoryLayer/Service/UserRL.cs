using ModelLayer.Models;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
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

        public async Task<UserEntity> RegisterUserRL(UserEntity userEntity)
        {
            await fundooContext.Users.AddAsync(userEntity);

            await fundooContext.SaveChangesAsync();

            return userEntity;
        }

        public async Task<UserEntity> LoginUser(LoginModel loginuser)
        {
            string email = loginuser.Email;

            UserEntity? user = await fundooContext.Users.FirstOrDefaultAsync(e => e.Email == email);

            return user!;
        }


        public async Task<ForgotPasswordResponseModel> ForgotPasswordAsyncRL(ForgotPasswordRequestModel req)
        {
            bool containsEmail = await fundooContext.Users.AnyAsync(e => e.Email == req.Email);

            if (containsEmail) return new ForgotPasswordResponseModel()
            {
                Message = "Email exists",
                Scuccess = true
            };

            return new ForgotPasswordResponseModel()
            {
                Message = "Email doesn't exists",
                Scuccess = false
            };
        }

        public async Task<ResetPasswordResponseModel> ResetPasswordAsyncRL(ResetPasswordRequestModel req)
        {
            var user = await fundooContext.Users.FirstOrDefaultAsync(e => e.Email == req.Email);

            if (user == null)
            {
                return new ResetPasswordResponseModel()
                {
                    Success = false,
                    Message = "user not found"
                };
            }

            user.Password = BCrypt.Net.BCrypt.HashPassword(req.NewPassword);

            await fundooContext.SaveChangesAsync();

            return new ResetPasswordResponseModel()
            {
                Success = true,
                Message = "Password Changed Successfully"
            };

        }
    }
}

