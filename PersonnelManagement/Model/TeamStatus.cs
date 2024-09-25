namespace PersonnelManagement.Model
{
    public class TeamStatus
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public ICollection<Team>? Teams { get; set; }
    }
}