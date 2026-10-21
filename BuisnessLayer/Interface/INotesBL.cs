using System;
using ModelLayer.Models;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;

namespace BuisnessLayer.Interface
{
	public interface INotesBL
	{
        public Task<NoteEntity> AddNoteBL(NoteModel note);
        public Task<NoteEntity?> FindNoteByIdBL(NotesIdModel _notesId, int userId);
        public Task<List<NoteEntity>> ViewAllNotesBL(int userId);
        public Task<NoteEntity?> DeleteNoteBL(NotesIdModel _notesId, int userId);
        public Task<int> ArchUnarchNoteBL(NotesIdModel _notesId, int userId);
        public Task<int> TrashUnTrashBL(NotesIdModel _notesId, int userId);
        public Task<NoteEntity> EditNoteBL(EditNoteModel editNoteModel, int userId);
    }
}

