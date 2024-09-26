namespace PersonnelManagement.Model
{
    public class Team
    {
        public long Id { get; set; }
        public string? CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public required string Name { get; set; }
        public int StatusId { get; set; }
        public required TeamStatus Status { get; set; }
        public long? LeaderId { get; set; }
        public Employee? Leader { get; set; }
        public ICollection<Employee>? Employees { get; set; }
        public ICollection<ProjectTeamDetail>? ProjectTeamDetails { get; set; }
    }
}