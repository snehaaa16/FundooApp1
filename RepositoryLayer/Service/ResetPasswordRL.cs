using Microsoft.EntityFrameworkCore;
using ModelLayer.Models;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;

namespace RepositoryLayer.Service
{
	public class ResetPasswordRL: IResetPasswordRL
    {
        FundooContext fundooContext;
        public ResetPasswordRL(FundooContext fundooContext)
		{
			this.fundooContext = fundooContext;
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
            try
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
            catch (Exception e)
            {
                return new ResetPasswordResponseModel()
                {
                    Success = false,
                    Message = "An error occurred while resetting the password"
                };
            }
        }
	}
}

