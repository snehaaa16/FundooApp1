using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BuisnessLayer.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ModelLayer.Models;

namespace BuisnessLayer.Service
{
	public class JwtService: IJwtService
    {
		private readonly IConfiguration _config;

        public JwtService(IConfiguration user)
		{
			_config = user;
		}
		
		public string GenerateToken(UserResponseModel user)
		{
			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
			var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
			var claims = new[]
			{
				new Claim(ClaimTypes.NameIdentifier,user.UserId.ToString()),
				new Claim(ClaimTypes.Email,user.Email),
				new Claim(ClaimTypes.Name,user.Fname)
            };
            var token = new JwtSecurityToken(
				issuer: _config["Jwt:Issuer"],
				audience: _config["Jwt:Audience"],
				claims: claims,
				expires: DateTime.UtcNow.AddMinutes(int.Parse(_config["Jwt:ExpiryMinutes"]!)),
				signingCredentials: credentials
				);

            return new JwtSecurityTokenHandler().WriteToken(token);

        }


	}
}

