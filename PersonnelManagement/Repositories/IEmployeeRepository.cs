using PersonnelManagement.Model;

namespace PersonnelManagement.Repositories
{
    public interface IEmployeeRepository
    {
        Task<Employee?> GetFullInforAsync(long id);
        void Update(Employee employee);
        Task SaveChangeAsync();
        Task<(ICollection<Employee>, int totalPages, int totalRecords)> GetPagedListAsync(int pageNumber, int pageSize);
        Task<(ICollection<Employee>, int, int)> FilterAsync(string? nameOrId, string? address,
            DateTime? fromDoB, DateTime? toDoB, double? fromSalary, double? toSalary, string? position,
            DateTime? fromStartDate, DateTime? toStartDate, int? departmentId, string? status, string? sortBy,
            int page, int pageSize);

    }
}
