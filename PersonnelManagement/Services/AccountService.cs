using PersonnelManagement.Model;
using PersonnelManagement.Repositories;

namespace PersonnelManagement.Services
{
    public class AccountService : IAccountService
    {
        private readonly IGenericCurdRepository<Account> _genericRepository;
        private readonly IAccountRepository _accRepo;

        public AccountService(IGenericCurdRepository<Account> repository, IAccountRepository accountRepository)
        {
            _genericRepository = repository;
            _accRepo = accountRepository;
        }

        public async Task<Account?> ValidateUserAsync(string email, string password)
        {
            var account = await _accRepo.GetUserAsync(email);
            if (account != null && password.Equals(account.Password))
            {
                return account;
            }
            return null;
        }

        public async Task<bool> ChangePasswordAsync(long accountId, string currentPassword, string newPassword)
        {
            var account = await _genericRepository.GetByIdAsync(accountId);
            if (account == null || account.Password != currentPassword)
            {
                return false;
            }
            return await _accRepo.UpdatePasswordAsync(accountId, newPassword);
        }

        public async Task<bool> ChangePasswordAsync(string email, string currentPassword, string newPassword)
        {
            var account = await _accRepo.GetAccountAsync(email);
            if (account == null || account.Password != currentPassword)
            {
                return false;
            }
            return await _accRepo.UpdatePasswordAsync(email, newPassword);
        }

        public async Task<bool> ExistAccountAsync(string email)
        {
            return await _accRepo.ExistAccountAsync(email);
        }
    }
}
