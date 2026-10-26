using System;
using ModelLayer.Models;
using RepositoryLayer.Entity;

namespace RepositoryLayer.Interface
{
	public interface INotesRL
	{
        public Task<NoteEntity> AddNoteRL(NoteEntity note);
        public Task<NoteEntity?> FindNoteByIdRL(NotesIdModel notesId, int userId);
        public Task<List<NoteEntity>> ViewAllNotesRL(int userId);
        public Task<NoteEntity?> DeleteNoteRL(NotesIdModel notesId, int userId);
        public Task<int> ArchUnarchNoteRL(NotesIdModel notesId, int userId);
        public Task<int> TrashUnTrashRL(NotesIdModel notesId, int userId);
        public Task<NoteEntity?> EditNoteRL(NoteEntity note, int userId);
    }
}

