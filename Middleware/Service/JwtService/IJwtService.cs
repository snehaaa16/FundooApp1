using System;
using ModelLayer.Models;

namespace Middleware.Service.JwtService
{
	public interface IJwtService
	{
        string GenerateToken(UserResponseModel user);

    }
}

