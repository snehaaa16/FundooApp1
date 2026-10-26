using System;
using MediatR;
using ModelLayer.Models;

namespace BuisnessLayer.Users.Command
{
	public class ForgotPasswordCommand : IRequest<ForgotPasswordResponseModel>
    {
        public ForgotPasswordRequestModel Request { get; }

        public ForgotPasswordCommand(ForgotPasswordRequestModel request)
        {
            Request = request;
        }
    }
}

