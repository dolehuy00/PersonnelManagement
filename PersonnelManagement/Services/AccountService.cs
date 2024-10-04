using PersonnelManagement.DTO;
using PersonnelManagement.Mappers;
using PersonnelManagement.Model;
using PersonnelManagement.Repositories;

namespace PersonnelManagement.Services
{
    public class AccountService : IAccountService
    {
        private readonly IGenericCurdRepository<Account> _genericAccRepo;
        private readonly IAccountRepository _accRepo;
        private AccountMapper _accMapper;

        public AccountService(IGenericCurdRepository<Account> repository, IAccountRepository accountRepository)
        {
            _genericAccRepo = repository;
            _accRepo = accountRepository;
            _accMapper = new AccountMapper();
        }

        public async Task<AccountDTO?> ValidateUserAsync(string email, string password)
        {
            var account = await _accRepo.GetAccountFullInforAsync(email);
            if (account != null && VerifyPassword(password, account.Password))
            {
                return _accMapper.ToDTO(account);
            }
            return null;
        }

        public async Task<bool> ChangePasswordAsync(long accountId, string currentPassword, string newPassword)
        {
            var account = await _genericAccRepo.GetByIdAsync(accountId);
            if (account == null || VerifyPassword(currentPassword, account.Password)!)
            {
                return false;
            }
            return await _accRepo.UpdatePasswordAsync(accountId, newPassword);
        }

        public async Task<bool> ChangePasswordAsync(string email, string currentPassword, string newPassword)
        {
            var account = await _accRepo.GetAccountAsync(email);
            if (account == null || VerifyPassword(currentPassword, account.Password)!)
            {
                return false;
            }
            return await _accRepo.UpdatePasswordAsync(email, newPassword);
        }

        public async Task<bool> ExistAccountAsync(string email)
        {
            return await _accRepo.ExistAccountAsync(email);
        }

        private static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        private static bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }

        async Task<AccountDTO> IAccountService.Add(AccountDTO accountDTO)
        {
            var exist = await _accRepo.ExistAccountAsync(accountDTO.Email);
            if (exist)
            {
                throw new Exception("Email already used in another account.");
            }
            await _genericAccRepo.AddAsync(_accMapper.ToModel(accountDTO));
            var account = await _accRepo.GetAccountFullInforAsync(accountDTO.Email);
            if (account != null)
            {
                return _accMapper.ToDTO(account);
            }
            throw new Exception("An error occurred while creating an account.");
        }
    }
}
