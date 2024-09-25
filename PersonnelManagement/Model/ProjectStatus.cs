namespace PersonnelManagement.Model
{
    public class ProjectStatus
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public ICollection<Project>? Projects { get; set; }
    }
}