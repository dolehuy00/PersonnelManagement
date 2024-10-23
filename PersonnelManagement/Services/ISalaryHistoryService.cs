using PersonnelManagement.DTO;

namespace PersonnelManagement.Services
{
    public interface ISalaryHistoryService
    {
        Task<SalaryHistoryDTO> Add(SalaryHistoryDTO accountDTO);
        Task<SalaryHistoryDTO> Edit(SalaryHistoryDTO accountDTO);
        Task Delete(long accountId);
        Task<SalaryHistoryDTO> Get(long accountId);
        Task<ICollection<SalaryHistoryDTO>> GetAll();
        Task<string[]> DeleteMany(long[] accountId);
        Task<(ICollection<SalaryHistoryDTO>, int, int)> GetPagesAsync(int pageNumber, int pageSize);
        Task<(ICollection<SalaryHistoryDTO>, int, int)> FilterAsync(SalaryHistoryFilterDTO filter);
        Task<SalaryHistoryDTO> GetByEmployee(long salaryHistoryId, long emplyeeId);
        Task<(ICollection<SalaryHistoryDTO>, int, int)> GetPagesByEmployeeAsync(
            int pageNumber, int pageSize, long employeeId);
    }
}
