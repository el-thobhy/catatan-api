using Microsoft.Data.SqlClient;
using NoteAppBackEnd.Models;
using NoteAppBackEnd.Repositories;
using NoteAppBackEnd.Services;

namespace NoteApp.Services
{
    public class NoteService : INoteService
    {
        private readonly string _connectionString;

        public NoteService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DbConn");
        }

        public int CreateNote(Note note)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            using var transaction = connection.BeginTransaction();
            int noteId = 0;
            try
            {
                var repo = new NoteRepository(connection, transaction);
                noteId = repo.Insert(note);

                transaction.Commit();
                return noteId;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception(ex.Message);
            }
        }

        public List<Note> GetAllNotesPaged(int pageNumber, int pageSize, string sortColumn, string sortDirection, string userId, string? search = null)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            using var transaction = connection.BeginTransaction();

            var repo = new NoteRepository(connection, transaction);
            return repo.GetAllNotePaged(pageNumber, pageSize, sortColumn, sortDirection, userId, search);
        }

        public Note? GetNoteById(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            using var transaction = connection.BeginTransaction();

            var repo = new NoteRepository(connection, transaction);
            Note? note = repo.GetById(id);

            if(note != null)
            {
                note.Entries = repo.GetEntriesByNoteId(id.ToString()) ?? [];
            }
            return note;
        }

        public void UpdateNote(Note note)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            using var transaction = connection.BeginTransaction();

            try
            {
                var repo = new NoteRepository(connection, transaction);
                repo.Update(note);

                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception(ex.Message);
            }
        }

        public void DeleteNote(int id, string? deletedBy)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            using var transaction = connection.BeginTransaction();

            try
            {
                var repo = new NoteRepository(connection, transaction);
                repo.Delete(id, deletedBy);

                transaction.Commit();
            }
            catch (Exception ex) 
            {
                transaction.Rollback();
                throw new Exception(ex.Message);
            }
        }
    }

}
