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
                .Include(acc => acc.Status)
                .FirstOrDefaultAsync(acc => acc.Email.Equals(email));
        }

        async Task<Account?> IAccountRepository.GetAccountAsync(string email)
        {
            return await _dataContext.Accounts.FirstOrDefaultAsync(acc => acc.Email.Equals(email));
        }

        public async Task<bool> ExistAccountAsync(string email)
        {
            return await _dataContext.Accounts.FirstOrDefaultAsync(acc => acc.Email.Equals(email)) != null;
        }

        async Task<bool> IAccountRepository.UpdatePasswordAsync(long accountId, string newPassword)
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

        async Task IAccountRepository.UpdatePasswordAsync(string email, string newPassword)
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

        async Task IAccountRepository.SaveChangesAsync()
        {
            await _dataContext.SaveChangesAsync();
        }
    }
}
