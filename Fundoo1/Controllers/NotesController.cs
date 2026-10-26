using System.Security.Claims;
using BuisnessLayer.Notes.Command;
using BuisnessLayer.Notes.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Models;
using RepositoryLayer.Entity;

namespace Fundoo1.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class NotesController : ControllerBase
{
    private readonly ISender _sender;
    private const string UserIdClaim = "user_id";

    public NotesController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateNote(NoteCreationModel note)
    {
        if (!TryGetUserId(out var userId))
            return Unauthorized();

        var entity = await _sender.Send(new CreateNoteCommand(note, userId));

        return Ok(new ResponseModel<NoteEntity>
        {
            Success = true,
            Message = "Note created successfully",
            Data = entity
        });
    }

    [HttpPost("ViewNotById")]
    public async Task<IActionResult> ViewNoteById(NotesIdModel notesId)
    {
        if (!TryGetUserId(out var userId))
            return Unauthorized();

        var note = await _sender.Send(new GetNoteByIdQuery(notesId, userId));

        if (note == null)
            return NotFound(new ResponseModel<NoteEntity>
            {
                Success = false,
                Message = "Note not Found"
            });

        return Ok(new ResponseModel<NoteEntity>
        {
            Success = true,
            Message = "Note Found",
            Data = note
        });
    }

    [HttpPost("ViewAllNotes")]
    public async Task<IActionResult> ViewAllNotes()
    {
        if (!TryGetUserId(out var userId))
            return Unauthorized();

        var notes = await _sender.Send(new GetAllNotesQuery(userId));

        return Ok(new ResponseModel<List<NoteEntity>>
        {
            Success = true,
            Message = "Notes Found",
            Data = notes
        });
    }

    [HttpPost("editNote")]
    public async Task<IActionResult> EditNote(EditNoteModel editNoteModel)
    {
        if (!TryGetUserId(out var userId))
            return Unauthorized();

        var editedNote = await _sender.Send(
            new EditNoteCommand(editNoteModel, userId));

        if (editedNote == null)
            return NotFound(new ResponseModel<NoteEntity>
            {
                Success = false,
                Message = "Note not found"
            });

        return Ok(new ResponseModel<NoteEntity>
        {
            Success = true,
            Message = "Note updated successfully",
            Data = editedNote
        });
    }

    [HttpPost("delete")]
    public async Task<IActionResult> DeleteNote(NotesIdModel notesId)
    {
        if (!TryGetUserId(out var userId))
            return Unauthorized();

        var deletedNote = await _sender.Send(new DeleteNoteCommand(notesId, userId));

        if (deletedNote == null)
            return NotFound(new ResponseModel<NoteEntity>
            {
                Success = false,
                Message = "Note not Found"
            });

        return Ok(new ResponseModel<NoteEntity>
        {
            Success = true,
            Message = "Note Deleted",
            Data = deletedNote
        });
    }

    [HttpPost("archive")]
    public async Task<IActionResult> ArchUnarchNote(NotesIdModel notesId)
    {
        if (!TryGetUserId(out var userId))
            return Unauthorized();

        var affectedRows = await _sender.Send(new ArchiveNoteCommand(notesId, userId));

        if (affectedRows == 0)
            return NotFound(new ResponseModel<NotesIdModel>
            {
                Success = false,
                Message = "Note not found",
                Data = notesId
            });

        return Ok(new ResponseModel<NotesIdModel>
        {
            Success = true,
            Message = "Note Archived/Unarchived",
            Data = notesId
        });
    }

    [HttpPost("trashing")]
    public async Task<IActionResult> TrashUnTrash(NotesIdModel notesId)
    {
        if (!TryGetUserId(out var userId))
            return Unauthorized();

        var affectedRows = await _sender.Send(new TrashNoteCommand(notesId, userId));

        if (affectedRows == 0)
            return NotFound(new ResponseModel<NotesIdModel>
            {
                Success = false,
                Message = "Note not found",
                Data = notesId
            });

        return Ok(new ResponseModel<NotesIdModel>
        {
            Success = true,
            Message = "Note Trashed/Untrashed",
            Data = notesId
        });
    }

    private bool TryGetUserId(out int userId)
    {
        return int.TryParse(User.FindFirstValue(UserIdClaim), out userId);
    }
}
