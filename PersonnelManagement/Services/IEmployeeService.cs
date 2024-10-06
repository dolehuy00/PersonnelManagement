using PersonnelManagement.DTO;

namespace PersonnelManagement.Services
{
    public interface IEmployeeService
    {
        public Task<EmployeeDTO> Add(EmployeeDTO accountDTO);
        public Task<EmployeeDTO> Edit(EmployeeDTO accountDTO);
        public Task Delete(long accountId);
        public Task<EmployeeDTO> Get(long accountId);
        public Task<ICollection<EmployeeDTO>> GetAll();
        public Task<string[]> DeleteMany(long[] accountId);
        Task<(ICollection<EmployeeDTO>, int totalPages, int totalRecords)> GetPagedListWithTotalPagesAsync(int pageNumber, int pageSize);
    }
}
