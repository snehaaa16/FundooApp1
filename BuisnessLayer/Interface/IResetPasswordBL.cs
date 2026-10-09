using System;
using ModelLayer.Models;

namespace BuisnessLayer.Interface
{
	public interface IResetPasswordBL
	{
        public Task<ResetPasswordResponseModel> ResetPasswordAsyncBL(ResetPasswordRequestModel req);

    }
}

