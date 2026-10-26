
using System;
using MediatR;
using ModelLayer.Models;
using RepositoryLayer.Entity;

namespace BuisnessLayer.Notes.Command
{
    public class CreateNoteCommand : IRequest<NoteEntity>
    {
        public NoteEntity Note { get; }

        public CreateNoteCommand(NoteCreationModel model, int userId)
        {
            Note = new NoteEntity
            {
                Title = model.Title,
                Description = model.Description,
                Colour = model.Colour,
                UserId = userId
            };
        }
    }
}

