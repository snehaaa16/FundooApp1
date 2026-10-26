using System;
using MediatR;
using ModelLayer;
using ModelLayer.Models;
using RepositoryLayer.Entity;

namespace BuisnessLayer.Users.Command
{
	public class CreateUserCommand : IRequest<UserResponseModel>
    {
        public UserEntity User { get; }

        public CreateUserCommand(RegistrationModel registrationModel)
        {
            User = new UserEntity();
            User.FirstName = registrationModel.FirstName;
            User.LastName = registrationModel.LastName;
            User.Email = registrationModel.Email;
            User.Password = BCrypt.Net.BCrypt.HashPassword(registrationModel.Password);
            User.PhoneNumber = registrationModel.ContactNo;
        }
    }
}

