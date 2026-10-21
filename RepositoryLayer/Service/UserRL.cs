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

        public async Task<ForgotPasswordResponseModel> SaveTokenRL(PasswordResetTokenModel tokenModel)
        {
            PasswordResetTokenEntity tokenEntity = new PasswordResetTokenEntity();
            tokenEntity.Email = tokenModel.Email;
            tokenEntity.ExpiryTime = tokenModel.ExpiryTime;
            tokenEntity.IsUsed = tokenModel.IsUsed;
            string hashToken = BCrypt.Net.BCrypt.HashPassword(tokenModel.Token);
            tokenEntity.Token = hashToken;

            fundooContext.Tokens.Add(tokenEntity);
            await fundooContext.SaveChangesAsync();

            return new ForgotPasswordResponseModel()
            {
                Scuccess = true,
                Message = "Token Saved Successfully"
            };
        }

        public ResetPasswordResponseModel ValidateToken(PasswordResetTokenEntity token)
        {
            if (token == null)
            {
                return new ResetPasswordResponseModel()
                {
                    Success = false,
                    Message = "No such token exists with specified Email"
                };
            }

            if (token.ExpiryTime <= DateTime.UtcNow)
            {
                return new ResetPasswordResponseModel()
                {
                    Success = false,
                    Message = "Token Expired"
                };
            }

            if (token.IsUsed)
            {
                return new ResetPasswordResponseModel()
                {
                    Success = false,
                    Message = "Token already used"
                };
            }

            return new ResetPasswordResponseModel()
            {
                Success = true,
                Message = "Validations Passed"
            };
        }

        public async Task<ResetPasswordResponseModel> UpdatePassword(ResetPasswordRequestModel req, PasswordResetTokenEntity token)
        {
            string hashPass = BCrypt.Net.BCrypt.HashPassword(req.NewPassword);

            var user = await fundooContext.Users.FirstOrDefaultAsync(e => e.Email == req.Email);

            if (user == null)
            {
                return new ResetPasswordResponseModel()
                {
                    Success = false,
                    Message = "user not found"
                };
            }

            user.Password = hashPass;
            token!.IsUsed = true;

            await fundooContext.SaveChangesAsync();

            return new ResetPasswordResponseModel()
            {
                Success = true,
                Message = "Password Changed Successfully"
            };
        }

        public async Task<ResetPasswordResponseModel> ResetPasswordAsyncRL(ResetPasswordRequestModel req)
        {
            var tokens = await fundooContext.Tokens.Where(e => e.Email == req.Email).ToListAsync();

            PasswordResetTokenEntity? token = tokens.FirstOrDefault(e => BCrypt.Net.BCrypt.Verify(req.Token, e.Token));

            ResetPasswordResponseModel validations = ValidateToken(token!);
            if (!validations.Success)
            {
                return validations;
            }

            if (req.ConfirmPassword != req.NewPassword)
            {
                return new ResetPasswordResponseModel()
                {
                    Success = false,
                    Message = "the password doesn't match"
                };
            }

            return await UpdatePassword(req, token!);
        }
    }
}

