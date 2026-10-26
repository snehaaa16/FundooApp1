using System;
using MediatR;
using ModelLayer.Models;

namespace BuisnessLayer.Notes.Command
{
    public class ArchiveNoteCommand : IRequest<int>
    {
        public NotesIdModel NotesId { get; }
        public int UserId { get; }

        public ArchiveNoteCommand(NotesIdModel notesId, int userId)
        {
            NotesId = notesId;
            UserId = userId;
        }
    }
}

