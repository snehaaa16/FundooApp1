using System;
using Microsoft.EntityFrameworkCore;
using ModelLayer.Models;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;

namespace RepositoryLayer.Service
{
	public class NotesRL: INotesRL
    {
        FundooContext _fundooContext;
		public NotesRL(FundooContext _fundooContext)
		{
			this._fundooContext = _fundooContext;
		}

		public static NoteModel ToNoteModel(NoteEntity noteEntity)
		{
			NoteModel note = new NoteModel();
            note.Title = noteEntity.Title;
            note.Description = noteEntity.Description;
            note.Colour = noteEntity.Description;
            note.UserId = noteEntity.UserId;
			return note;
        }

        public static NoteEntity ToNoteEntity(NoteModel noteModel)
        {
            NoteEntity noteEntity = new NoteEntity();
            noteEntity.Title = noteModel.Title;
            noteEntity.Description = noteModel.Description;
            noteEntity.Colour = noteModel.Description;
            noteEntity.UserId = noteModel.UserId;
            return noteEntity;
        }

        public async Task<NoteEntity> AddNoteRL(NoteModel note)
		{
            NoteEntity noteEntity = ToNoteEntity(note);
            var entity = await _fundooContext.Notes.AddAsync(noteEntity);
            await _fundooContext.SaveChangesAsync();
            return entity.Entity;
		}

        public async Task<NoteEntity?> FindNoteByIdRL(NotesIdModel _notesId, int userId)
        {
            NoteEntity? noteEntity = await _fundooContext.Notes.FirstOrDefaultAsync(e => e.UserId == userId && e.NoteId == _notesId.NoteId && !e.IsDeleted);
            return noteEntity;
        }

        public async Task<List<NoteEntity>> ViewAllNotesRL(int userId)
        {
            var notes = await _fundooContext.Notes.Where(e => e.UserId == userId && !e.IsArchived && !e.IsDeleted).ToListAsync();
            return notes;
        }

        public async Task<NoteEntity?> DeleteNoteRL(NotesIdModel _notesId, int userId)
        {
            NoteEntity? noteEntity = await _fundooContext.Notes.FirstOrDefaultAsync(e => e.UserId == userId && e.NoteId == _notesId.NoteId);
            if (noteEntity == null) return null;
            NoteEntity? deletedEntity= _fundooContext.Notes.Remove(noteEntity).Entity;
            await _fundooContext.SaveChangesAsync();
            return deletedEntity;
        }

        public async Task<int> ArchUnarchNoteRL(NotesIdModel _notesId,int userId)
        {
            NoteEntity? noteEntity = await  _fundooContext.Notes.FirstOrDefaultAsync(e => e.UserId==userId && e.NoteId == _notesId.NoteId);
            if (noteEntity == null) return 0;
            noteEntity.IsArchived = !noteEntity.IsArchived;
            return await _fundooContext.SaveChangesAsync();
        }

        public async Task<int> TrashUnTrashRL(NotesIdModel _notesId, int userId)
        {
            NoteEntity? noteEntity = await _fundooContext.Notes.FirstOrDefaultAsync(e => e.UserId == userId && e.NoteId == _notesId.NoteId);

            if (noteEntity == null) return 0;

            noteEntity.IsDeleted = !noteEntity.IsDeleted;

            return await _fundooContext.SaveChangesAsync();
        }

        public async Task<NoteEntity> EditNoteRL(EditNoteModel _editNoteModel,int userId)
        {
            NoteEntity? noteEntity = await _fundooContext.Notes.FirstOrDefaultAsync(e => e.UserId == userId && e.NoteId == _editNoteModel.NoteId);

            if (noteEntity == null) return null;

            noteEntity!.Colour = _editNoteModel.Colour;
            noteEntity!.Description = _editNoteModel.Description;
            noteEntity!.Title = _editNoteModel.Title;

            await _fundooContext.SaveChangesAsync();

            return noteEntity;

        }
    }
}

