using System;
using MediatR;
using ModelLayer.Models;
using RepositoryLayer.Entity;

namespace BuisnessLayer.Notes.Command
{
    public class DeleteNoteCommand : IRequest<NoteEntity?>
    {
        public NotesIdModel NotesId { get; }
        public int UserId { get; }

        public DeleteNoteCommand(NotesIdModel notesId, int userId)
        {
            NotesId = notesId;
            UserId = userId;
        }
    }
}

