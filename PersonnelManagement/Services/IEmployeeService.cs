using PersonnelManagement.DTO;
using PersonnelManagement.DTO.Filter;

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
        Task<ICollection<EmployeeDTO>> SearchNameOrIdAsync(string keyword);
        Task<(ICollection<EmployeeDTO>, int totalPages, int totalRecords)> FilterAsync(
            EmployeeFilterDTO filter);
        Task<bool> Lock(long id);
        Task<bool> UnLock(long id);
        Task UpdateImageAsync(long id, string fileUrl);
    }
}
