using PersonnelManagement.Model;

namespace PersonnelManagement.Repositories
{
    public interface ISalaryHistoryRepository
    {
        Task<(ICollection<SalaryHistory>, int, int)> FilterAsync(
            string? sortByDate, long? employeeId, int page, int pageSize);
        Task<SalaryHistory?> GetByEmployeeAsync(long salaryHistoryId, long emplyeeId);
        Task<SalaryHistory?> GetFullInforAsync(long id);
        Task<ICollection<SalaryHistory>> GetFullInforAsync();
        Task<(ICollection<SalaryHistory>, int, int)> GetPagedListAsync(
            int pageNumber, int pageSize);
        Task<(ICollection<SalaryHistory>, int, int)> GetPagedListByEmployeeAsync(
            int pageNumber, int pageSize, long employeeId);
    }
}
