using System;
using BuisnessLayer.Interface;
using MediatR;
using ModelLayer.Models;
using RepositoryLayer.Entity;
using RepositoryLayer.Service;

namespace BuisnessLayer.Users.Command
{
	public class CreateUserHandler: IRequestHandler<CreateUserCommand, UserResponseModel>
    {
        private IUserBL _userBL;

        public CreateUserHandler(IUserBL userBL )
		{
            _userBL = userBL;
		}

        public async Task<UserResponseModel> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            UserEntity savedUser = await _userBL.RegisterUserBL(command.User);

            return new UserResponseModel
            {
                UserId = savedUser.UserId,
                Fname = savedUser.FirstName,
                Lname = savedUser.LastName,
                Email = savedUser.Email,
                ContactNo = savedUser.PhoneNumber
            };
        }
    }
}

