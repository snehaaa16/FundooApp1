using System;
using BuisnessLayer.Interface;
using BuisnessLayer.Notes.Command;
using MediatR;
using RepositoryLayer.Entity;

namespace BuisnessLayer.Notes.Handler
{
    public class EditNoteHandler : IRequestHandler<EditNoteCommand, NoteEntity?>
    {
        private readonly INotesBL _notesBL;

        public EditNoteHandler(INotesBL notesBL)
        {
            _notesBL = notesBL;
        }

        public async Task<NoteEntity?> Handle(
            EditNoteCommand request, CancellationToken cancellationToken)
        {
            if (request.UserId <= 0)
                throw new ArgumentException("Invalid user.");

            if (request.Note.NoteId <= 0)
                throw new ArgumentException("Invalid note ID.");

            if (string.IsNullOrWhiteSpace(request.Note.Title))
                throw new ArgumentException("Note title is required.");

            return await _notesBL.EditNoteBL(request.Note, request.UserId);
        }
    }
}

