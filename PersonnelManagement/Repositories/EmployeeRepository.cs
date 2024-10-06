using Microsoft.EntityFrameworkCore;
using PersonnelManagement.Data;
using PersonnelManagement.Model;

namespace PersonnelManagement.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly PersonnelDataContext _dataContext;

        public EmployeeRepository(PersonnelDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        async Task<Employee?> IEmployeeRepository.GetFullInforAsync(long id)
        {
            return await _dataContext.Employees
               .Include(e => e.Status)
               .Include(e => e.Department)
               .FirstOrDefaultAsync(e => e.Id == id);
        }

        async Task IEmployeeRepository.UpdateAsync(Employee employee)
        {
            var existingEntity = _dataContext.Employees.Local.FirstOrDefault(e => e.Id == employee.Id);
            if (existingEntity != null)
            {
                _dataContext.Entry(existingEntity).State = EntityState.Detached;
            }

            _dataContext.Employees.Update(employee);
            await _dataContext.SaveChangesAsync();
        }
        public async Task<(ICollection<Employee>, int totalPages, int totalRecords)> GetPagedListAsync(int pageNumber, int pageSize)
        {
            var skip = (pageNumber - 1) * pageSize;
            var totalRecords = await _dataContext.Employees.CountAsync();
            var totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
            var pagedList = await _dataContext.Employees
                .OrderBy(e => e.Id)
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync();

            return (pagedList, totalPages, totalRecords);
        }
    }
}