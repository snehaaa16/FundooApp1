using System;
namespace ModelLayer.Models
{
	public class EmailRequestModel
	{
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsHtml { get; set; }
    }
}

