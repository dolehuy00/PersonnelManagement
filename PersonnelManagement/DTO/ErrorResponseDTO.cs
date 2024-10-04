namespace PersonnelManagement.DTO
{
    public class ErrorResponseDTO
    {
        public string? Type { get; set; }
        public string? Title { get; set; }
        public int Status { get; set; }
        public required dynamic Errors { get; set; }
        public string? TraceId { get; set; }
    }
}
