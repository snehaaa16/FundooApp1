using System;
using ModelLayer.Models;

namespace RepositoryLayer.Interface
{
	public interface IResetPasswordRL
	{
        public Task<ResetPasswordResponseModel> ResetPasswordAsyncRL(ResetPasswordRequestModel req);

    }
}

