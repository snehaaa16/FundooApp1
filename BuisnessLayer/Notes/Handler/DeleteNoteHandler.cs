using System;
using BuisnessLayer.Interface;
using BuisnessLayer.Notes.Command;
using MediatR;
using RepositoryLayer.Entity;

namespace BuisnessLayer.Notes.Handler
{
    public class DeleteNoteHandler : IRequestHandler<DeleteNoteCommand, NoteEntity?>
    {
        private readonly INotesBL _notesBL;

        public DeleteNoteHandler(INotesBL notesBL)
        {
            _notesBL = notesBL;
        }

        public async Task<NoteEntity?> Handle(
            DeleteNoteCommand request, CancellationToken cancellationToken)
        {
            if (request.UserId <= 0)
                throw new ArgumentException("Invalid user.");

            if (request.NotesId.NoteId <= 0)
                throw new ArgumentException("Invalid note ID.");

            return await _notesBL.DeleteNoteBL(request.NotesId, request.UserId);
        }
    }
}

