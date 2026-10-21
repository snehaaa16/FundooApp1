using System;
namespace ModelLayer.Models
{
	public class EmailResponseModel
	{
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}

