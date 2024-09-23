namespace PersonnelManagement.Model
{
    public class Project
    {
        public long Id { get; set; }
        public string? CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public required string Name { get; set; }
        public string? Detail { get; set; }
        public DateTime Duration { get; set; }
        public DateTime StartDate { get; set; }
        public required ProjectStatus Status { get; set; }
        public ICollection<Assignment>? Assignments { get; set; }
        public ICollection<ProjectTeamDetail>? ProjectTeamDetails { get; set; }
    }
}