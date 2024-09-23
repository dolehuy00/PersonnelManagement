namespace PersonnelManagement.Model
{
    public class ProjectTeamDetail
    {
        public long Id { get; set; }
        public string? CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int PriorityLevel { get; set; }
        public required Project Project { get; set; }
        public required Team Team { get; set; }
    }
}
