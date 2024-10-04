namespace PersonnelManagement.DTO
{
    public class SuccessResponseDTO<T> where T : class
    {
        public string? Title { get; set; }
        public int Status { get; set; }
        public ICollection<T>? Results { get; set; }
    }
}
