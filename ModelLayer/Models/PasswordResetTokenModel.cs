using System;
namespace ModelLayer.Models
{
	public class PasswordResetTokenModel
	{
		public string Email { get; set; }
		public string Token { get; set; }
		public DateTime ExpiryTime { get; set; }
		public bool IsUsed { get; set; }
	}
}

