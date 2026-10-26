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

        public async Task<NoteEntity> AddNoteRL(NoteEntity note)
        {
            await _fundooContext.Notes.AddAsync(note);
            await _fundooContext.SaveChangesAsync();
            return note;
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
            NoteEntity? noteEntity = await  _fundooContext.Notes.FirstOrDefaultAsync(e => e.UserId==userId && e.NoteId == _notesId.NoteId && !e.IsDeleted);
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


        public async Task<NoteEntity?> EditNoteRL(NoteEntity note, int userId)
        {
            var existingNote = await _fundooContext.Notes.FirstOrDefaultAsync(e => e.UserId == userId && e.NoteId == note.NoteId && !e.IsDeleted);

            if (existingNote == null)
                return null;

            existingNote.Title = note.Title;
            existingNote.Description = note.Description;
            existingNote.Colour = note.Colour;

            await _fundooContext.SaveChangesAsync();

            return existingNote;
        }
    }
}

