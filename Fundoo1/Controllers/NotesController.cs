using System.Security.Claims;
using BuisnessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using ModelLayer.Models;
using RepositoryLayer.Entity;

namespace Fundoo1.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	[Authorize]
	public class NotesController: ControllerBase
    {
		private const string UserIdClaim = "user_id";
        private INotesBL _notesBL;
        public NotesController(INotesBL _notesBL)
        {
			this._notesBL = _notesBL;
        }

		[HttpPost]
		[Route("create")]
		public async Task<IActionResult> CreateNote(NoteCreationModel note)
		{
			if(!TryGetUserId(out var UserId))
			{
				return Unauthorized();
			}

			NoteModel noteModel = new NoteModel();
			noteModel.Colour = note.Colour;
			noteModel.Description = note.Description;
			noteModel.Title = note.Title;
			noteModel.UserId = UserId;

			var entity = await _notesBL.AddNoteBL(noteModel);
			if (entity == null)
			{
				return BadRequest(new ResponseModel<NoteEntity>
				{
					Success = false,
					Message = "Saving Note to Database Failed"
				});
			}
			return Ok(new ResponseModel<NoteEntity>
			{
				Success = true,
				Message = "Note created successfully",
				Data = entity
			});
        }

        [HttpPost]
        [Route("ViewNotById")]
		public async Task<IActionResult> ViewNoteById(NotesIdModel _notesId)
		{
            if (!TryGetUserId(out var UserId))
            {
                return Unauthorized();
            }

            var note = await _notesBL.FindNoteByIdBL(_notesId, UserId);

            if(note==null) return NotFound(new ResponseModel<NoteEntity>
            {
                Success = false,
                Message = "Note not Found"
            });

            return Ok(new ResponseModel<NoteEntity>
            {
                Success = true,
                Message="Note Found",
                Data=note
            }) ;
        }

        [HttpPost]
        [Route("ViewAllNotes")]
        public async Task<IActionResult> ViewAllNotes()
        {
            if (!TryGetUserId(out var UserId))
            {
                return Unauthorized();
            }

            var notes = await _notesBL.ViewAllNotesBL(UserId);
            return Ok(new ResponseModel<List<NoteEntity>>
            {
                Success=true,
                Message="Notes Found",
                Data=notes
            });
        }

        [HttpPost]
        [Route("editNote")]
        public async Task<IActionResult> EditNote(EditNoteModel editNoteModel)
        {
            if (!TryGetUserId(out var UserId))
            {
                return Unauthorized();
            }

            var editedNote = await _notesBL.EditNoteBL(editNoteModel, UserId);

            if(editedNote==null) return BadRequest(new ResponseModel<NoteEntity>
            {
                Success = false,
                Message = "note not found",
            });

            return Ok(new ResponseModel<NoteEntity>
            {
                Success=true,
                Message="note updated successfully",
                Data=editedNote
            });
        }

        [HttpPost]
		[Route("delete")]
		public async Task<IActionResult> DeleteNote(NotesIdModel _notesId)
		{
            if (!TryGetUserId(out var UserId))
            {
                return Unauthorized();
            }

			var deletedNote = await _notesBL.DeleteNoteBL(_notesId, UserId);

			if (deletedNote == null) return NotFound(new ResponseModel<NoteEntity>
			{
				Success=false,
				Message="Note not Found"
			});

            return Ok(new ResponseModel<NoteEntity>
            {
                Success = true,
                Message = "Note Deleted",
				Data=deletedNote
            });
        }

        [HttpPost]
        [Route("archive")]
        public async Task<IActionResult> ArchUnarchNote(NotesIdModel _notesId)
		{
            if (!TryGetUserId(out var UserId))
            {
                return Unauthorized();
            }

			int affectedRows = await _notesBL.ArchUnarchNoteBL(_notesId, UserId);

			if (affectedRows == 0) return NotFound(new ResponseModel<NotesIdModel>
			{
				Success = false,
				Message="Note not found",
				Data= _notesId
            }) ;

            return Ok(new ResponseModel<NotesIdModel>
            {
                Success = true,
                Message = "Note Archived/Unarchived",
                Data = _notesId
            });
        }

        [HttpPost]
        [Route("trashing")]
        public async Task<IActionResult> TrashUnTrash(NotesIdModel _notesId)
        {
            if (!TryGetUserId(out var UserId))
            {
                return Unauthorized();
            }

            int affectedRows = await _notesBL.TrashUnTrashBL(_notesId, UserId);

            if (affectedRows == 0) return NotFound(new ResponseModel<NotesIdModel>
            {
                Success = false,
                Message = "Note not found",
                Data = _notesId
            });

            return Ok(new ResponseModel<NotesIdModel>
            {
                Success = true,
                Message = "Note Trashed/Untrashed",
                Data = _notesId
            });
        }

        private bool TryGetUserId(out int UserId)
		{
			return int.TryParse(User.FindFirstValue(UserIdClaim), out UserId);
		}

    }
}

