using System;
using MediatR;
using ModelLayer.Models;
using RepositoryLayer.Entity;

namespace BuisnessLayer.Notes.Queries
{
    public class GetNoteByIdQuery : IRequest<NoteEntity?>
    {
        public NotesIdModel NotesId { get; }
        public int UserId { get; }

        public GetNoteByIdQuery(NotesIdModel notesId, int userId)
        {
            NotesId = notesId;
            UserId = userId;
        }
    }
}

