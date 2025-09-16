namespace NoteAppBackEnd.Models
{
    public class CreateNoteVM
    {
        public string Title { get; set; }
        public string LocationOfProject { get; set; }
        public string ClientName { get; set; }
    }
    public class EditNoteVM : CreateNoteVM { }
}
