using Microsoft.Data.SqlClient;
using NoteAppBackEnd.Models;
using NoteAppBackEnd.Repositories;

namespace NoteAppBackEnd.Services
{
    public class DailyEntryService : IDailyEntryService
    {
        private readonly string _connectionString;

        public DailyEntryService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DbConn");
        }

        public int AddEntry(DailyEntry entry)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            using var transaction = connection.BeginTransaction();

            try
            {
                var repo = new DailyEntryRepository(connection, transaction);
                int entryId = repo.Insert(entry);
                transaction.Commit();
                return entryId;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception(ex.Message);
            }
        }

        public void UpdateEntry(DailyEntry entry)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            using var transaction = connection.BeginTransaction();

            try
            {
                var repo = new DailyEntryRepository(connection, transaction);
                repo.Update(entry);
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception(ex.Message);
            }
        }
        

        public void UpdateDate(int id, string date, string modified_by)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            using var transaction = connection.BeginTransaction();

            try
            {
                var repo = new DailyEntryRepository(connection, transaction);
                repo.UpdateDate(id, date, modified_by);
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception(ex.Message);
            }
        }

        public void DeleteEntry(int id, string? deletedBy)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            using var transaction = connection.BeginTransaction();

            try
            {
                var repo = new DailyEntryRepository(connection, transaction);
                repo.Delete(id, deletedBy);
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception(ex.Message);
            }
        }

        public DailyEntry? GetEntryById(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            using var transaction = connection.BeginTransaction();

            var repo = new DailyEntryRepository(connection, transaction);
            return repo.GetById(id);
        }

        public List<DailyEntry> GetEntriesByNoteId(int noteId)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            using var transaction = connection.BeginTransaction();

            var repo = new DailyEntryRepository(connection, transaction);
            return repo.GetByNoteId(noteId);
        }
        public List<DailyEntry> GetEntryListDateByNoteId(int noteId)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            using var transaction = connection.BeginTransaction();

            var repo = new DailyEntryRepository(connection, transaction);
            return repo.GetEntriesByNoteId(noteId);
        }

        public List<DailyEntry> GetEntriesAll(string userId)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            using var transaction = connection.BeginTransaction();

            var repo = new DailyEntryRepository(connection, transaction);
            return repo.GetAllNote(userId);
        }
        public List<DailyEntry> GetAllNotesEntriesPaged(int pageNumber, int pageSize, string sortColumn, string sortDirection, string userId, string? search = null)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            using var transaction = connection.BeginTransaction();

            var repo = new DailyEntryRepository(connection, transaction);
            return repo.GetAllNoteEntriesPaged(pageNumber, pageSize, sortColumn, sortDirection, userId, search);
        }

    }
}
