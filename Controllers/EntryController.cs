using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NoteAppBackEnd.Models;
using NoteAppBackEnd.Security;
using NoteAppBackEnd.Services;
using System.Reflection;

namespace NoteAppBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ReadableBodyStream(Roles = "home")]
    public class EntryController : ControllerBase
    {
        private readonly IDailyEntryService _noteEntryService;
        private readonly IEmailHelper _emailHelper;
        private readonly IConfiguration _configuration;

        public EntryController(IDailyEntryService noteEntryService, IConfiguration configuration)
        {
            _configuration = configuration;
            _noteEntryService = noteEntryService;
            _emailHelper = new EmailHelper(_configuration);
        }

        [HttpGet("GetAllNoteEntriesPaged")]
        public IActionResult GetAllNoteEntriesPaged(int pageNumber, int pageSize, string sortColumn, string sortDirection, string userId, string? search = null)
        {
            try
            {
                List<DailyEntry> entries = new();
                string UserId = ClaimsContext.UserName();

                entries = _noteEntryService.GetAllNotesEntriesPaged(pageNumber, pageSize, sortColumn, sortDirection, userId, search);

                return Ok(new ResponseModel
                {
                    Success = true,
                    Message = "Notes retrieved successfully",
                    Data = entries,
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

        [HttpGet("GetListDateByNoteId/{id}")]
        public IActionResult GetListDateByNoteId(int id)
        {
            try
            {
                List<DailyEntry> entries = _noteEntryService.GetEntryListDateByNoteId(id);
                return Ok(new ResponseDefault
                {
                    Success = true,
                    Message = "Success Get Data",
                    Data = entries
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

        [HttpGet("GetEntryById/{id}")]
        public IActionResult GetEntryById(int id)
        {
            try
            {
                DailyEntry entry = _noteEntryService.GetEntryById(id);
                return Ok(new ResponseDefault
                {
                    Success = true,
                    Message = "Success Get Data",
                    Data = entry
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

        [HttpPost("AddEntry")]
        public IActionResult Create(CreateEntryVM entryVm) {
            try
            {
                string UserId = ClaimsContext.UserName();
                DailyEntry entry = new()
                {
                    Content = entryVm.Content,
                    Title_Note = entryVm.TitleNote,
                    Created_by = UserId,
                    Date = entryVm.Date,
                    NoteId = entryVm.NoteId,
                    WH_start = entryVm.WHStart,
                    WH_end = entryVm.WHEnd,
                    OT_start = entryVm.OTStart,
                    OT_end = entryVm.OTEnd,
                    Total_WH = entryVm.TotalWH,
                    Total_OT = entryVm.TotalOT,
                    Status_absen = entryVm.StatusAbsen,
                    UserId = UserId
                };
                int entryId = _noteEntryService.AddEntry(entry);
                entry.Id = entryId;
                return Ok(new ResponseDefault
                {
                    Success = true,
                    Message = "Success Create Entry",
                    Data = entry
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
        [HttpPut("UpdateEntry/{id}")]
        public IActionResult Update(EditEntryVM entryVm, int id) {
            try
            {
                string UserId = ClaimsContext.UserName();
                DailyEntry entry = new()
                {
                    Content = entryVm.Content,
                    Title_Note = entryVm.TitleNote,
                    Created_by = UserId,
                    Id = id,
                    Date = entryVm.Date,
                    NoteId = entryVm.NoteId,
                    WH_start = entryVm.WHStart,
                    WH_end = entryVm.WHEnd,
                    OT_start = entryVm.OTStart,
                    OT_end = entryVm.OTEnd,
                    Total_WH = entryVm.TotalWH,
                    Total_OT = entryVm.TotalOT,
                    Status_absen = entryVm.StatusAbsen,
                    UserId = UserId
                };
                _noteEntryService.UpdateEntry(entry);
                return Ok(new ResponseDefault
                {
                    Success = true,
                    Message = "Success Update Entry",
                    Data = entry
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
        [HttpPut("UpdateDate/{id}")]
        public IActionResult UpdateDate(int id, string date) {
            try
            {
                string UserId = ClaimsContext.UserName();
                _noteEntryService.UpdateDate(id, date, UserId);
                return Ok(new ResponseDefault
                {
                    Success = true,
                    Message = "Success Update Date",
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
        [HttpDelete("DeleteEntry/{id}")]
        public IActionResult DeleteEntry(int id) {
            try
            {
                string userId = ClaimsContext.UserName();
                _noteEntryService.DeleteEntry(id, userId);
                return Ok(new ResponseDefault
                {
                    Success = true,
                    Message = "Success Delete Date",
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

        [HttpPost("ShareNote/{id}")]
        public IActionResult ShareNote(int id, ShareEmailModel request)
        {
            try
            {
                DailyEntry entry = _noteEntryService.GetEntryById(id);
                request.BodyMail = entry.Content;
                _emailHelper.SendEmail(request);
                return Ok(new ResponseDefault
                {
                    Success = true,
                    Message = "Success Share Note",
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
