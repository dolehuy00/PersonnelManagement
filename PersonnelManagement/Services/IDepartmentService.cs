using PersonnelManagement.DTO;
using PersonnelManagement.DTO.Filter;

namespace PersonnelManagement.Services
{
    public interface IDepartmentService
    {
        Task<(ICollection<DepartmentDTO>, int totalPages, int totalRecords)> FilterAsync(
            DepartmentFilterDTO departmentFilter);

        Task<DepartmentDTO> Add(DepartmentDTO departmentDTO);
        Task<DepartmentDTO> Edit(DepartmentDTO departmentDTO);
        Task Delete(long departmentId);
        Task<DepartmentDTO?> Get(long departmentIds);
        Task<ICollection<DepartmentDTO>> GetAll();
        Task<string[]> DeleteMany(long[] departmentId);
    }
}
