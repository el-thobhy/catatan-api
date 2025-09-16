namespace NoteAppBackEnd.Models
{
    public class ResponseModel
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public object Data { get; set; }

        public int Pages { get; set; }
    }
    public class ResponseDefault
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public object Data { get; set; }
    }

}
