using PersonnelManagement.Model;

namespace PersonnelManagement.Repositories
{
    public interface ISalaryHistoryRepository
    {
        Task<(ICollection<SalaryHistory>, int, int)> FilterAsync(
            string? sortByDate, long? employeeId, int page, int pageSize);
        Task<(ICollection<SalaryHistory>, int, int)> GetPagedListByEmployeeAsync(
            int pageNumber, int pageSize, long employeeId);
    }
}
