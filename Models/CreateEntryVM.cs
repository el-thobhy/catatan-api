namespace NoteAppBackEnd.Models
{
    public class CreateEntryVM
    {
        public int NoteId { get; set; }
        public string? TitleNote { get; set; }
        public string Date { get; set; }
        public string? WHStart { get; set; }
        public string? WHEnd { get; set; }
        public string? OTStart { get; set; }
        public string? OTEnd { get; set; }
        public string? TotalWH { get; set; }
        public string? TotalOT { get; set; }
        public string? StatusAbsen { get; set; }
        public string? Content { get; set; }
    }
    public class EditEntryVM : CreateEntryVM { }
}
