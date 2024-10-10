namespace PersonnelManagement.Model
{
    public class Department
    {
        public long Id { get; set; }
        public required string Name { get; set; }
        public string? TaskDetail { get; set; }
        public required string Status { get; set; }
        public long? LeaderId { get; set; }
        public Employee? Leader { get; set; }
        public ICollection<DeptAssignment>? DeptAssignments { get; set; }
        public ICollection<Employee>? Employees { get; set; }
    }
}