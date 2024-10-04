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

        async Task<bool> IAccountRepository.UpdatePasswordAsync(string email, string newPassword)
        {
            var account = await _dataContext.Accounts.FirstOrDefaultAsync(acc => acc.Email.Equals(email));
            if (account != null)
            {
                account.Password = newPassword;
                await _dataContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
