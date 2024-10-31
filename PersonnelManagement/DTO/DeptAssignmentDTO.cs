using PersonnelManagement.Model;

namespace PersonnelManagement.DTO
{
    public class DeptAssignmentDTO
    {
        public long Id { get; set; }
        public int PriotityLevel { get; set; }
        public string? MainTaskDetail { get; set; }
        public long ProjectId { get; set; }
        public long DepartmentId { get; set; }
        public ICollection<AssignmentDTO>? AssignmentDTOs { get; set; }
    }
}
