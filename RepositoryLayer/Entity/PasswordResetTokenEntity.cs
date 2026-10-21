using System;
using System.ComponentModel.DataAnnotations;

namespace RepositoryLayer.Entity
{
	public class PasswordResetTokenEntity
	{
        [Key]
        public int Id { get; set; }
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Token { get; set; } = string.Empty;
        [Required]
        public DateTime ExpiryTime { get; set; }
        [Required]
        public bool IsUsed { get; set; }
    }
}

