using PersonnelManagement.Model;

namespace PersonnelManagement.Repositories
{
    public interface IEmployeeRepository
    {
        public Task<Employee?> GetFullInforAsync(long id);
        Task UpdateAsync(Employee employee);
        Task<(ICollection<Employee>, int totalPages, int totalRecords)> GetPagedListAsync(int pageNumber, int pageSize);
        Task<ICollection<Employee>> FilterAsync();
    }
}
