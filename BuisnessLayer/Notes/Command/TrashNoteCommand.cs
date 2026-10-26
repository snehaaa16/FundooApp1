using System;
using MediatR;
using ModelLayer.Models;

namespace BuisnessLayer.Notes.Command
{
    public class TrashNoteCommand : IRequest<int>
    {
        public NotesIdModel NotesId { get; }
        public int UserId { get; }

        public TrashNoteCommand(NotesIdModel notesId, int userId)
        {
            NotesId = notesId;
            UserId = userId;
        }
    }
}

