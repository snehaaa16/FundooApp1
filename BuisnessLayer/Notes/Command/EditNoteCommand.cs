using System;
using MediatR;
using ModelLayer.Models;
using RepositoryLayer.Entity;

namespace BuisnessLayer.Notes.Command
{
    public class EditNoteCommand : IRequest<NoteEntity?>
    {
        public NoteEntity Note { get; }
        public int UserId { get; }

        public EditNoteCommand(EditNoteModel model, int userId)
        {
            Note = new NoteEntity
            {
                NoteId = model.NoteId,
                Title = model.Title,
                Description = model.Description,
                Colour = model.Colour,
                UserId = userId
            };

            UserId = userId;
        }
    }
}

