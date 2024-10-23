namespace PersonnelManagement.DTO
{
    public class AssignmentFilterDTO
    {
        public string? SortBy { get; set; }
        public string? Status { get; set; }
        public long? ResponsiblePesonId { get; set; }
        public long? ProjectId { get; set; }
        public long? DepartmentId { get; set; }
        public required int Page { get; set; }
        public required int PageSize { get; set; }
    }
}
