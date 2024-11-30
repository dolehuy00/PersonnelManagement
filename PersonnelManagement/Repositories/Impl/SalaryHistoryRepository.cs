using Microsoft.EntityFrameworkCore;
using PersonnelManagement.Data;
using PersonnelManagement.Model;

namespace PersonnelManagement.Repositories.Impl
{
    public class SalaryHistoryRepository : ISalaryHistoryRepository
    {
        private readonly PersonnelDataContext _dataContext;

        public SalaryHistoryRepository(PersonnelDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<(ICollection<SalaryHistory>, int, int)> FilterAsync(
            string? sortByDate, long? employeeId, int page, int pageSize)
        {
            // Loc theo status, xep theo date, loc theo id employee.
            var query = _dataContext.SalaryHistories
                .Include(s => s.Employee)
                .AsQueryable();

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
