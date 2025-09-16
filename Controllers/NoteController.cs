using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NoteAppBackEnd.Models;
using NoteAppBackEnd.Security;
using NoteAppBackEnd.Services;
using System.Security.Claims;
using System.Web.Helpers;

namespace NoteAppBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ReadableBodyStream(Roles = "home")]
    public class NoteController : ControllerBase
    {
        private readonly INoteService _noteService;
        public NoteController(INoteService noteService)
        {
            _noteService = noteService;
        }
        [HttpGet("GetAllNotePaged")]
        public IActionResult GetAllNotes(int pageNumber, int pageSize, string sortColumn, string sortDirection, string userId, string? search = null)
        {
            try
            {
                List<Note> notes = [];
                List<DailyEntry> entries = [];
                string UserId = ClaimsContext.UserName();

                notes = _noteService.GetAllNotesPaged(pageNumber, pageSize, sortColumn, sortDirection, userId, search);
                
                return Ok(new ResponseModel
                {
                    Success = true,
                    Message = "Notes retrieved successfully",
                    Data = notes,
                    Pages = pageNumber
                });
            }
            catch (Exception e)
            {
                return BadRequest(new ResponseModel
                {
                    Success = false,
                    Message = e.Message,
                    Data = null
                });
            }
        }
        [HttpPost("CreateNote")]
        public IActionResult CreateNote(CreateNoteVM noteVm)
        {
            try
            {
                string UserId = ClaimsContext.UserName();
                Note note = new()
                {
                    Created_by = UserId,
                    UserId = UserId,
                    Created_on = DateTime.Now,
                    Title = noteVm.Title,
                    ClientName = noteVm.ClientName,
                    LocationOfProject = noteVm.LocationOfProject
                };
                int noteId = _noteService.CreateNote(note);
                note.Id = noteId.ToString();
                return Ok(new ResponseDefault
                {
                    Success = true,
                    Message = "Success Create note",
                    Data = note
                });
            }
            catch (Exception e)
            {
                return BadRequest(new ResponseDefault
                {
                    Success = false,
                    Message = e.Message,
                    Data = null
                });
            }
        }
        [HttpGet("GetByNoteId/{id}")]
        public IActionResult GetByNoteId(int id)
        {
            try
            {
                Note note = _noteService.GetNoteById(id);
                return Ok(new ResponseDefault
                {
                    Success = true,
                    Message = "Success Get Data",
                    Data = note
                });
            }
            catch (Exception e)
            {
                return BadRequest(new ResponseDefault
                {
                    Success = false,
                    Message = e.Message,
                    Data = null
                });
            }
        }

        [HttpPut("EditNote/{id}")]
        public IActionResult EditNote(EditNoteVM noteVm, int id)
        {
            try
            {
                string UserId = ClaimsContext.UserName();
                Note note = new()
                {
                    Id = id.ToString(),
                    Created_by = UserId,
                    UserId = UserId,
                    Created_on = DateTime.Now,
                    Title = noteVm.Title,
                    ClientName = noteVm.ClientName,
                    LocationOfProject = noteVm.LocationOfProject
                };
                _noteService.UpdateNote(note);
                return Ok(new ResponseDefault
                {
                    Success = true,
                    Message = "Success Create note",
                    Data = note
                });
            }
            catch (Exception e)
            {
                return BadRequest(new ResponseDefault
                {
                    Success = false,
                    Message = e.Message,
                    Data = null
                });
            }
        }

        [HttpDelete("DeleteNote/{id}")]
        public IActionResult DeleteNote(int id)
        {
            try
            {
                _noteService.DeleteNote(id, ClaimsContext.UserName());
                return Ok(new ResponseDefault
                {
                    Success = true,
                    Message = "The data has been deleted",
                    Data = null
                });
            }
            catch (Exception e)
            {
                return BadRequest(new ResponseDefault
                {
                    Success = false,
                    Message = e.Message,
                    Data = null
                });
            }
        }

    }
}
