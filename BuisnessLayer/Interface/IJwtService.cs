using System;
using ModelLayer.Models;

namespace BuisnessLayer.Interface
{
	public interface IJwtService
	{
		string GenerateToken(UserResponseModel user);
	}
}

