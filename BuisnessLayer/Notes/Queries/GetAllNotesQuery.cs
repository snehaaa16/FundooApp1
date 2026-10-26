using System;
using MediatR;
using RepositoryLayer.Entity;

namespace BuisnessLayer.Notes.Queries
{
    public class GetAllNotesQuery : IRequest<List<NoteEntity>>
    {
        public int UserId { get; }

        public GetAllNotesQuery(int userId)
        {
            UserId = userId;
        }
    }
}

