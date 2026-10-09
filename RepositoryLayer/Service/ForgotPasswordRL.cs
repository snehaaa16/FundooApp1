using Microsoft.EntityFrameworkCore;
using ModelLayer.Models;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;

namespace RepositoryLayer.Service
{
	public class ForgotPasswordRL: IForgotPasswordRL
    {
        FundooContext fundooContext;

		public ForgotPasswordRL(FundooContext fundooContext)
		{
			this.fundooContext = fundooContext;
		}

        public async Task<ForgotPasswordResponseModel> ForgotPasswordAsyncRL(ForgotPasswordRequestModel req)
		{
			bool containsEmail= await fundooContext.Users.AnyAsync(e => e.Email == req.Email);

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
			try
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
            catch (Exception ex)
            {
                return new ForgotPasswordResponseModel()
                {
                    Scuccess = false,
                    Message = ex.InnerException?.Message ?? ex.Message
                };
            }
			
        }
    }
}