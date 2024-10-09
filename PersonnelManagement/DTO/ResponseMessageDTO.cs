namespace PersonnelManagement.DTO
{
    public class ResponseMessageDTO
    {
        public ResponseMessageDTO(string? title, ICollection<string>? messages)
        {
            Title = title;
            Messages = messages;
            Status = 200;
        }

        public ResponseMessageDTO(string? title, int status, ICollection<string> messages)
        {
            Title = title;
            Status = status;
            Messages = messages;
        }

        public string? Title { get; set; }
        public int Status { get; set; }
        public ICollection<string>? Messages { get; set; }
    }
}
