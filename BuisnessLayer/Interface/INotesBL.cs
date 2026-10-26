using System;
using ModelLayer.Models;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;

namespace BuisnessLayer.Interface
{
	public interface INotesBL
	{
        public Task<NoteEntity> AddNoteBL(NoteEntity note);
        public Task<NoteEntity?> FindNoteByIdBL(NotesIdModel notesId, int userId);
        public Task<List<NoteEntity>> ViewAllNotesBL(int userId);
        public Task<NoteEntity?> EditNoteBL(NoteEntity note, int userId);
        public Task<NoteEntity?> DeleteNoteBL(NotesIdModel notesId, int userId);
        public Task<int> ArchUnarchNoteBL(NotesIdModel notesId, int userId);
        public Task<int> TrashUnTrashBL(NotesIdModel notesId, int userId);
    }
}

