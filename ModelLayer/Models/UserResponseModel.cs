namespace ModelLayer.Models
{
	public class UserResponseModel
	{
		public int UserId { get; set; }
		public string Fname { get; set; } = string.Empty;
        public string Lname { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string ContactNo { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }
}