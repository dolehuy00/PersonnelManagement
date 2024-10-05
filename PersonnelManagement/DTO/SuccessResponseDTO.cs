namespace PersonnelManagement.DTO
{
    public class SuccessResponseDTO<T> where T : class
    {
        public string? Title { get; set; }
        public int Status { get; set; } = 200;
        public ICollection<T>? Results { get; set; }
        public int Page { get; set; } = 1;
        public int TotalPage { get; set; } = 1;
        public int TotalCount { get; set; } = 1;
    }
}
