using System;
using BuisnessLayer.Interface;
using BuisnessLayer.Notes.Command;
using MediatR;
using ModelLayer.Models;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;

namespace BuisnessLayer.Notes.Handler
{
    public class CreateNoteHandler : IRequestHandler<CreateNoteCommand, NoteEntity>
    {
        private readonly INotesBL _notesBL;

        public CreateNoteHandler(INotesBL notesBL)
        {
            _notesBL = notesBL;
        }

        public async Task<NoteEntity> Handle(CreateNoteCommand request, CancellationToken cancellationToken)
        {
            if (request.Note.UserId <= 0)
                throw new ArgumentException("Invalid user.");

            if (string.IsNullOrWhiteSpace(request.Note.Title))
                throw new ArgumentException("Note title is required.");

            return await _notesBL.AddNoteBL(request.Note);
        }
    }
}

