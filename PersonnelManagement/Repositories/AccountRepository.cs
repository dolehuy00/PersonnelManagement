using Microsoft.EntityFrameworkCore;
using PersonnelManagement.Data;
using PersonnelManagement.Model;

namespace PersonnelManagement.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly PersonnelDataContext _dataContext;

        public AccountRepository(PersonnelDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Account?> GetAccountFullInforAsync(string email)
        {
            return await _dataContext.Accounts
                .Include(acc => acc.Employee)
                .Include(acc => acc.Role)
                .FirstOrDefaultAsync(acc => acc.Email.Equals(email));
        }

        public async Task<Account?> GetAccountAsync(string email)
        {
            return await _dataContext.Accounts.FirstOrDefaultAsync(acc => acc.Email.Equals(email));
        }

        public async Task<bool> ExistAccountAsync(string email)
        {
            return await _dataContext.Accounts.FirstOrDefaultAsync(acc => acc.Email.Equals(email)) != null;
        }

        public async Task<bool> UpdatePasswordAsync(long accountId, string newPassword)
        {
            var account = await _dataContext.Accounts.FirstOrDefaultAsync(acc => acc.Id == accountId);
            if (account != null)
            {
                account.Password = newPassword;
                await _dataContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task UpdatePasswordAsync(string email, string newPassword)
        {
            var account = await _dataContext.Accounts.
                FirstOrDefaultAsync(acc => acc.Email.Equals(email)) ?? throw new Exception("Account doesn't exist.");
            account.Password = newPassword;
            await _dataContext.SaveChangesAsync();
        }

        public async Task<(ICollection<Account>, int totalPages, int totalRecords)> GetPagedListAsync(int pageNumber, int pageSize)
        {
            var skip = (pageNumber - 1) * pageSize;
            var totalRecords = await _dataContext.Accounts.CountAsync();
            var totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
            var pagedList = await _dataContext.Accounts
                .OrderBy(e => e.Id)
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync();

            return (pagedList, totalPages, totalRecords);
        }

        public async Task<(ICollection<Account>, int totalPages, int totalRecords)> FilterAsync(string? keyword,
            string? sortByEmail, string? filterByStatus, int? filterByRole, string? keywordByEmployee, int pageNumber, int pageSize)
        {
            // Tim theo email hoac id, xep theo email, loc theo status, loc theo role, tim kiem theo ten hoac id employee
            var query = _dataContext.Accounts
                .Include(a => a.Employee)
                .AsQueryable();

            // Tìm kiếm theo email hoặc id
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(a => a.Email.Contains(keyword) || a.Id.ToString().Contains(keyword));
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
            if (filterByRole.HasValue)
            {
                query = query.Where(a => a.RoleId == filterByRole.Value);
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
