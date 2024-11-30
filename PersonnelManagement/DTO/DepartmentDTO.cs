namespace PersonnelManagement.DTO
{
    public class DepartmentDTO
    {
        public long Id { get; set; }
        public required string Name { get; set; }
        public string? TaskDetail { get; set; }
        public string? Status { get; set; }
        public long? LeaderId { get; set; }
        public String? LeaderName { get; set; }
        public ICollection<long>? DeptAssignmentsId { get; set; }
        public ICollection<long>? EmployeeIds { get; set; }
    }
}
