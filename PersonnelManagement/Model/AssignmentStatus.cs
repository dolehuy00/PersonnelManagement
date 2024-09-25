namespace PersonnelManagement.Model
{
    public class AssignmentStatus
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public ICollection<Assignment>? Assignments { get; set; }
    }
}