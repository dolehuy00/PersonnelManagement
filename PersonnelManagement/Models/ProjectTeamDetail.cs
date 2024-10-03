namespace PersonnelManagement.Model
{
    public class ProjectTeamDetail
    {
        public int PriorityLevel { get; set; }
        public long ProjectId { get; set; }
        public long TeamId { get; set; }
        public required Project Project { get; set; }
        public required Team Team { get; set; }
    }
}
