using PersonnelManagement.Model;

namespace PersonnelManagement.DTO
{
    public class ProjectDTO
    {
        public long Id { get; set; }
        public required string Name { get; set; }
        public string? Detail { get; set; }
        public DateTime Duration { get; set; }
        public DateTime StartDate { get; set; }
        public required string Status { get; set; }
        public ICollection<DeptAssignmentDTO>? DeptAssignmentDTOs { get; set; }
    }
}