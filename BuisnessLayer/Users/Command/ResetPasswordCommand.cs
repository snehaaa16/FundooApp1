using System;
using MediatR;
using ModelLayer.Models;

namespace BuisnessLayer.Users.Command
{
    public class ResetPasswordCommand : IRequest<ResetPasswordResponseModel>
    {
        public ResetPasswordRequestModel Request { get; }

        public ResetPasswordCommand(ResetPasswordRequestModel request)
        {
            Request = request;
        }
    }
}

