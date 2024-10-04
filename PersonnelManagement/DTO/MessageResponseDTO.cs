namespace PersonnelManagement.DTO
{
    public class MessageResponseDTO
    {
        public string? Title { get; set; }
        public int Status { get; set; }
        public required ICollection<string> Messages { get; set; }
    }
}
