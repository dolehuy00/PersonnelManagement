using PersonnelManagement.Model;

namespace PersonnelManagement.Repositories
{
    public interface IEmployeeRepository
    {
        public Task<Employee?> GetFullInforAsync(long id);
        Task UpdateAsync(Employee employee);
        Task<(ICollection<Employee>, int totalPages, int totalRecords)> GetPagedListAsync(int pageNumber, int pageSize);
        Task<(ICollection<Employee>, int, int)> FilterAsync(string? nameOrId, string? address,
            DateTime? fromDoB, DateTime? toDoB, double? fromSalary, double? toSalary, string? position,
            DateTime? fromStartDate, DateTime? toStartDate, int? departmentId, string? status, string? sortBy,
            int page, int pageSize);

    }
}
