using System;
using BuisnessLayer.Interface;
using ModelLayer.Models;
using RepositoryLayer.Interface;

namespace BuisnessLayer.Service
{
	public class ResetPasswordBL: IResetPasswordBL
    {
		private IResetPasswordRL _resetPasswordRL;
		public ResetPasswordBL(IResetPasswordRL _resetPasswordRL)
		{
			this._resetPasswordRL = _resetPasswordRL;
		}

		public async Task<ResetPasswordResponseModel> ResetPasswordAsyncBL(ResetPasswordRequestModel req)
		{
			return await _resetPasswordRL.ResetPasswordAsyncRL(req);
		}

    }
}

