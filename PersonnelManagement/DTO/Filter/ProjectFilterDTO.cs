using PersonnelManagement.Model;

namespace PersonnelManagement.DTO.Filter
{
    public class ProjectFilterDTO : BaseFilter
    {
        public long? Id { get; set; }
        public string? Name { get; set; }
        public string? Status { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? StartDate { get; set; }
    }
}
