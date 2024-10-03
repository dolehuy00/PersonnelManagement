namespace PersonnelManagement.Model
{
    public class Project
    {
        public long Id { get; set; }
        public required string Name { get; set; }
        public string? Detail { get; set; }
        public DateTime Duration { get; set; }
        public DateTime StartDate { get; set; }
        public int StatusId { get; set; }
        public required ProjectStatus Status { get; set; }
        public ICollection<DeptAssignment>? DeptAssignments { get; set; }
    }
}