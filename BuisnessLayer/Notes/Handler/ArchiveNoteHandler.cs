using System;
using BuisnessLayer.Interface;
using BuisnessLayer.Notes.Command;
using MediatR;

namespace BuisnessLayer.Notes.Handler
{
    public class ArchiveNoteHandler : IRequestHandler<ArchiveNoteCommand, int>
    {
        private readonly INotesBL _notesBL;

        public ArchiveNoteHandler(INotesBL notesBL)
        {
            _notesBL = notesBL;
        }

        public async Task<int> Handle(
            ArchiveNoteCommand request, CancellationToken cancellationToken)
        {
            if (request.UserId <= 0)
                throw new ArgumentException("Invalid user.");

            if (request.NotesId.NoteId <= 0)
                throw new ArgumentException("Invalid note ID.");

            return await _notesBL.ArchUnarchNoteBL(request.NotesId, request.UserId);
        }
    }
}

