namespace PersonnelManagement.Model
{
    public class DeptAssignment
    {
        public long Id { get; set; }
        public int PriotityLevel { get; set; }
        public string? MainTaskDetail { get; set; }
        public long ProjectId { get; set; }
        public long DepartmentId { get; set; }
        public required Project Project { get; set; }
        public required Department Department { get; set; }
        public ICollection<Assignment>? Assignments { get; set; }
    }
}