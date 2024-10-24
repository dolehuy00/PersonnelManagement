using PersonnelManagement.Model;

namespace PersonnelManagement.DTO.Filter
{
    public class DepartmentFilterDTO : BaseFilter
    {
        public long? Id { get; set; }
        public string? Name { get; set; }
        public string? Status { get; set; }
    }
}
