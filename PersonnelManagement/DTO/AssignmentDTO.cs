namespace PersonnelManagement.DTO
{
    public class AssignmentDTO
    {
        public long Id { get; set; }
        public string? Detail { get; set; }
        public string? Name { get; set; }
        public int PriotityLevel { get; set; }
        public required string Status { get; set; }
        public long? ResponsiblePesonId { get; set; }
        public string? ResponsiblePesonName { get; set; }
        public long DeptAssignmentId { get; set; }
        public long? ProjectId { get; set; }
        public string? ProjectName { get; set; }
    }
}
