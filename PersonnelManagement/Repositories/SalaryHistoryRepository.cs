using Microsoft.EntityFrameworkCore;
using PersonnelManagement.Data;
using PersonnelManagement.Model;

namespace PersonnelManagement.Repositories
{
    public class SalaryHistoryRepository : ISalaryHistoryRepository
    {
        private readonly PersonnelDataContext _dataContext;

        public SalaryHistoryRepository(PersonnelDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<(ICollection<SalaryHistory>, int, int)> FilterAsync(
            string? sortByDate, string? status, long? employeeId, int page, int pageSize)
        {
            // Loc theo status, xep theo date, loc theo id employee.
            var query = _dataContext.SalaryHistories
                .Include(s => s.Employee)
                .AsQueryable();

            // Lọc theo status nếu status có giá trị
            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(s => s.Status.Contains(status));
            }

            // Lọc theo id employee nếu employee có giá trị
            if (employeeId.HasValue)
            {
                query = query.Where(s => s.EmployeeId == employeeId.Value);
            }

            // Tính tổng số record (trước khi phân trang)
            var totalRecords = await query.CountAsync();

            // Sắp xếp theo date nếu sortByDate có giá trị là asc hoặc desc
            if (!string.IsNullOrEmpty(sortByDate))
            {
                if (sortByDate.Equals("asc", StringComparison.OrdinalIgnoreCase))
                {
                    query = query.OrderBy(s => s.Date);
                }
                else if (sortByDate.Equals("dec", StringComparison.OrdinalIgnoreCase))
                {
                    query = query.OrderByDescending(s => s.Date);
                }
            }
            else
            {
                query = query.OrderByDescending(s => s.Id);
            }

            var skip = (page - 1) * pageSize;
            var items = await query
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync();
            var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
            return (items, totalPages, totalRecords);
        }

        public async Task<SalaryHistory?> GetByEmployeeAsync(long salaryHistoryId, long emplyeeId)
        {
            return await _dataContext.SalaryHistories
               .Include(s => s.Employee)
               .FirstOrDefaultAsync(s => s.Id == salaryHistoryId && s.EmployeeId == emplyeeId);
        }

        public async Task<SalaryHistory?> GetFullInforAsync(long id)
        {
            return await _dataContext.SalaryHistories
               .Include(s => s.Employee)
               .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<ICollection<SalaryHistory>> GetFullInforAsync()
        {
            return await _dataContext.SalaryHistories
               .Include(s => s.Employee)
               .ToListAsync();
        }

        public async Task<(ICollection<SalaryHistory>, int, int)> GetPagedListAsync(
            int pageNumber, int pageSize)
        {
            var skip = (pageNumber - 1) * pageSize;
            var totalRecords = await _dataContext.SalaryHistories.CountAsync();
            var totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
            var pagedList = await _dataContext.SalaryHistories
                .OrderBy(s => s.Id)
                .Skip(skip)
                .Take(pageSize)
                .Include(s => s.Employee)
                .ToListAsync();

            return (pagedList, totalPages, totalRecords);
        }

        public async Task<(ICollection<SalaryHistory>, int, int)> GetPagedListByEmployeeAsync(
            int pageNumber, int pageSize, long employeeId)
        {
            var query = _dataContext.SalaryHistories
                .Include(s => s.Employee)
                .Where(s => s.EmployeeId == employeeId)
                .AsQueryable();

            var skip = (pageNumber - 1) * pageSize;
            var totalRecords = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
            var pagedList = await query
                .OrderByDescending(s => s.Date)
                .Skip(skip)
                .Take(pageSize)
                .Include(s => s.Employee)
                .ToListAsync();

            return (pagedList, totalPages, totalRecords);
        }
    }
}
