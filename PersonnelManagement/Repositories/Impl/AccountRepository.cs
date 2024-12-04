using Microsoft.EntityFrameworkCore;
using PersonnelManagement.Data;
using PersonnelManagement.Model;

namespace PersonnelManagement.Repositories.Impl
{
    public class AccountRepository : GenericRepository<Account>, IAccountRepository
    {
        public AccountRepository(PersonnelDataContext dataContext) : base(dataContext) { }

        public async Task<Account?> GetAccountFullInforAsync(string email)
        {
            return await _context.Accounts
                .Include(acc => acc.Employee)
                    .ThenInclude(emp => emp.LeaderOfDepartments)
                .Include(acc => acc.Role)
                .FirstOrDefaultAsync(acc => acc.Email.Equals(email));
        }

        public async Task<(ICollection<Account>, int totalPages, int totalRecords)> FilterAsync(string? keyword,
            string? sortByEmail, string? filterByStatus, string? filterByRole, string? keywordByEmployee, int pageNumber, int pageSize)
        {
            // Tim theo email hoac id, xep theo email, loc theo status, loc theo role, tim kiem theo ten hoac id employee
            var query = _context.Accounts
                .Include(a => a.Employee)
                .AsQueryable();

            // Tìm kiếm theo email hoặc id
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(a => a.Email.Contains(keyword) || a.Id.ToString().Equals(keyword));
            }

            // Tìm kiếm theo tên hoặc id Employee
            if (!string.IsNullOrEmpty(keywordByEmployee))
            {
                query = query.Where(a =>
                    a.Employee.Fullname.Contains(keywordByEmployee) ||
                    a.Employee.Id.ToString().Contains(keywordByEmployee));
            }

            // Lọc theo status nếu filterByStatus có giá trị
            if (!string.IsNullOrEmpty(filterByStatus))
            {
                query = query.Where(a => a.Status.Contains(filterByStatus));
            }

            // Lọc theo role nếu filterByRole có giá trị
            if (!string.IsNullOrEmpty(filterByRole))
            {
                query = query.Where(a => a.Role.Name.Contains(filterByRole) || a.RoleId.ToString().Equals(filterByRole));
            }

            // Tính tổng số record (trước khi phân trang)
            var totalRecords = await query.CountAsync();

            // Sắp xếp theo email nếu sortByEmail có giá trị là asc hoặc desc
            if (!string.IsNullOrEmpty(sortByEmail))
            {
                if (sortByEmail.Equals("asc", StringComparison.OrdinalIgnoreCase))
                {
                    query = query.OrderBy(a => a.Email);
                }
                else if (sortByEmail.Equals("dec", StringComparison.OrdinalIgnoreCase))
                {
                    query = query.OrderByDescending(a => a.Email);
                }
            }
            else
            {
                query = query.OrderByDescending(a => a.Id);
            }

            //Include
            query = query.Include(a => a.Role);

            var skip = (pageNumber - 1) * pageSize;
            var items = await query.Skip(skip).Take(pageSize).ToListAsync();
            var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
            return (items, totalPages, totalRecords);
        }
    }
}
