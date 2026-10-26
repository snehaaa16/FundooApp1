using BuisnessLayer.Interface;
using MediatR;
using Middleware.Service.JwtService;
using ModelLayer.Models;
using RepositoryLayer.Service;

namespace BuisnessLayer.Users.Command
{
	public class LoginHandler : IRequestHandler<LoginCommand, UserResponseModel?>
    {
        private IJwtService _jwtService;
        private IUserBL _userBL;
        public LoginHandler(IJwtService jwtService,IUserBL userBL)
		{
            _jwtService = jwtService;
            _userBL = userBL;
		}

		public async Task<UserResponseModel?> Handle(LoginCommand command,CancellationToken cancellationToken)
		{
            var user = await _userBL.LoginUserBL(command._loginModel);
            if (user == null) return null;

            bool isValid = BCrypt.Net.BCrypt.Verify(command._loginModel.Password, user.Password);
            if (!isValid) return null;

            UserResponseModel response = new UserResponseModel();
            response.UserId = user.UserId;
            response.ContactNo = user.PhoneNumber;
            response.Email = user.Email;
            response.Fname = user.FirstName;
            response.Lname = user.LastName;
            
            response.Token = _jwtService.GenerateToken(response);

            return response;
        }
	}
}

