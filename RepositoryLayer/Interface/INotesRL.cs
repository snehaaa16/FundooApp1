using System;
using ModelLayer.Models;
using RepositoryLayer.Entity;

namespace RepositoryLayer.Interface
{
	public interface INotesRL
	{
        public Task<NoteEntity> AddNoteRL(NoteModel note);
        public Task<NoteEntity?> FindNoteByIdRL(NotesIdModel _notesId, int userId);
        public Task<List<NoteEntity>> ViewAllNotesRL(int userId);
        public Task<NoteEntity?> DeleteNoteRL(NotesIdModel _notesId, int userId);
        public Task<int> ArchUnarchNoteRL(NotesIdModel _notesId, int userId);
        public Task<int> TrashUnTrashRL(NotesIdModel _notesId, int userId);
        public Task<NoteEntity> EditNoteRL(EditNoteModel _editNoteModel, int userId);
    }
}

