using System;
using ModelLayer.Models;

namespace BuisnessLayer.Notes.Command
{
	public class TrashUntrashCommand
	{
		public NotesIdModel NotesId;
		public int UserId;

        public TrashUntrashCommand(NotesIdModel notesId, int userId)
		{
			NotesId = notesId;
			UserId = userId;
		}
	}
}

