namespace NoteAppBackEnd.Models
{
    public class Note: BaseProperties
    {
        public string? Id { get; set; }
        public string Title { get; set; }
        public List<DailyEntry> Entries { get; set; } = new();
        public string UserId { get; set; }
        public string ClientName { get; set; }
        public string LocationOfProject { get; set; }
    }

}
