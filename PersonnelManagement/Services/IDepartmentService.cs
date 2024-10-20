using PersonnelManagement.DTO;
using PersonnelManagement.DTO.Filter;

namespace PersonnelManagement.Services
{
    public interface IDepartmentService
    {
        Task<(ICollection<DepartmentDTO>, int totalPages, int totalRecords)> FilterAsync(
            DepartmentFilterDTO departmentFilter);
    }
}
