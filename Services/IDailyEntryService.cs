using NoteAppBackEnd.Models;

namespace NoteAppBackEnd.Services
{

    public interface IDailyEntryService
    {
        int AddEntry(DailyEntry entry);
        void UpdateEntry(DailyEntry entry);
        void UpdateDate(int id, string date, string modified_by);
        void DeleteEntry(int id, string? deletedBy);
        DailyEntry? GetEntryById(int id);
        List<DailyEntry> GetEntriesByNoteId(int noteId);
        List<DailyEntry> GetEntryListDateByNoteId(int noteId);
        List<DailyEntry> GetEntriesAll(string userId);
        List<DailyEntry> GetAllNotesEntriesPaged(int pageNumber, int pageSize, string sortColumn, string sortDirection, string userId, string? search = null);
    }

}
