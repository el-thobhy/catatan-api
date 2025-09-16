using NoteAppBackEnd.Models;

namespace NoteAppBackEnd.Services
{
    public interface INoteService
    {
        int CreateNote(Note note);
        List<Note> GetAllNotesPaged(int pageNumber, int pageSize, string sortColumn, string sortDirection, string userId, string? search = null);
        Note? GetNoteById(int id);
        void UpdateNote(Note note);
        void DeleteNote(int id, string? deletedBy);
    }

}
