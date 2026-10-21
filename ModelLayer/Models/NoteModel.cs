using System;
namespace ModelLayer.Models
{
	public class NoteModel
	{
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Colour { get; set; } = string.Empty;

        public int UserId { get; set; }
    }
}

