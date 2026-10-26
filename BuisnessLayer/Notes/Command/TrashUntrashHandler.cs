using BuisnessLayer.Interface;
using ModelLayer.Models;
using RepositoryLayer.Interface;
using RepositoryLayer.Service;

namespace BuisnessLayer.Notes.Command
{
	public class TrashUntrashHandler
	{
		private INotesBL _notesBL;
        public TrashUntrashHandler(INotesBL notesBL)
		{
            _notesBL = notesBL;
        }

		public async Task<ResponseModel<NotesIdModel>> Handle(TrashUntrashCommand command)
		{
			int affectedRows= await _notesBL.TrashUnTrashBL(command.NotesId, command.UserId);
            if (affectedRows == 0) return new ResponseModel<NotesIdModel>
            {
                Success = false,
                Message = "Note not found",
                Data = command.NotesId
            };

            return new ResponseModel<NotesIdModel>
            {
                Success = true,
                Message = "Note Trashed/Untrashed",
                Data = command.NotesId
            };
        }
	}
}

