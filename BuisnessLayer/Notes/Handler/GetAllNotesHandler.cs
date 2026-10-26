using System;
using BuisnessLayer.Interface;
using BuisnessLayer.Notes.Queries;
using MediatR;
using RepositoryLayer.Entity;

namespace BuisnessLayer.Notes.Handler
{
    public class GetAllNotesHandler : IRequestHandler<GetAllNotesQuery, List<NoteEntity>>
    {
        private readonly INotesBL _notesBL;

        public GetAllNotesHandler(INotesBL notesBL)
        {
            _notesBL = notesBL;
        }

        public async Task<List<NoteEntity>> Handle(
            GetAllNotesQuery request, CancellationToken cancellationToken)
        {
            if (request.UserId <= 0)
                throw new ArgumentException("Invalid user.");

            return await _notesBL.ViewAllNotesBL(request.UserId);
        }
    }
}

