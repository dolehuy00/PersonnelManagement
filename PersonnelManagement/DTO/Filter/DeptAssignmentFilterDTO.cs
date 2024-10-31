namespace PersonnelManagement.DTO.Filter
{
    public class DeptAssignmentFilterDTO : BaseFilter
    {
        public long? Id { get; set; }
        public long? projectId { get; set; }
        public long? departmentId { get; set; }
    }
}
