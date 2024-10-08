using PersonnelManagement.DTO;

namespace PersonnelManagement.Services
{
    public interface IEmployeeService
    {
        Task<EmployeeDTO> Add(EmployeeDTO accountDTO);
        Task<EmployeeDTO> Edit(EmployeeDTO accountDTO);
        Task Delete(long accountId);
        Task<EmployeeDTO> Get(long accountId);
        Task<ICollection<EmployeeDTO>> GetAll();
        Task<string[]> DeleteMany(long[] accountId);
        Task<(ICollection<EmployeeDTO>, int totalPages, int totalRecords)> GetPagesAsync(
            int pageNumber, int pageSize);
        Task<(ICollection<EmployeeDTO>, int totalPages, int totalRecords)> FilterAsync(
            EmployeeFilterDTO filter);
    }
}
