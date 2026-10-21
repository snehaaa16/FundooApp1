using BuisnessLayer.Interface;
using ModelLayer.Models;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using RepositoryLayer.Service;

namespace BuisnessLayer.Service
{
	public class NotesBL: INotesBL
    {
		private INotesRL _notesRL;
		public NotesBL(INotesRL _notesRL)
		{
			this._notesRL = _notesRL;
		}

		public async Task<NoteEntity> AddNoteBL(NoteModel note)
		{
			return await _notesRL.AddNoteRL(note);
		}

        public async Task<NoteEntity?> FindNoteByIdBL(NotesIdModel _notesId, int userId)
        {
            return await _notesRL.FindNoteByIdRL(_notesId, userId);
        }

        public async Task<NoteEntity?> DeleteNoteBL(NotesIdModel _notesId, int userId)
        {
            return await _notesRL.DeleteNoteRL(_notesId,userId);
        }

        public async Task<int> ArchUnarchNoteBL(NotesIdModel _notesId,int userId)
		{
			return await _notesRL.ArchUnarchNoteRL(_notesId,userId);
        }

        public async Task<int> TrashUnTrashBL(NotesIdModel _notesId, int userId)
        {
            return await _notesRL.TrashUnTrashRL(_notesId, userId);
        }

        public async Task<List<NoteEntity>> ViewAllNotesBL(int userId)
        {
            return await _notesRL.ViewAllNotesRL(userId);
        }

        public async Task<NoteEntity> EditNoteBL(EditNoteModel editNoteModel,int userId)
        {
            return await _notesRL.EditNoteRL(editNoteModel,userId);
        }


    }
}

