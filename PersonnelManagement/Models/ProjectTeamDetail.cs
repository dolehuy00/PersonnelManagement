namespace PersonnelManagement.Model
{
    public class ProjectTeamDetail
    {
        public string? CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int PriorityLevel { get; set; }
        public long ProjectId { get; set; }
        public long TeamId { get; set; }
        public required Project Project { get; set; }
        public required Team Team { get; set; }
    }
}
