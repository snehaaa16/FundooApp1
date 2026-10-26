using System;
using MediatR;
using ModelLayer.Models;

namespace BuisnessLayer.Users.Command
{
	public class LoginCommand: IRequest<UserResponseModel?>
    {
        public LoginModel _loginModel;

		public LoginCommand(LoginModel loginModel)
		{
			_loginModel = loginModel;
		}
	}
}

