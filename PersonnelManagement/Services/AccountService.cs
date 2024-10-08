using MovieAppApi.Service;
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
            if (account == null || !VerifyPassword(currentPassword, account.Password))
            {
                return false;
            }
            account.Password = HashPassword(newPassword);
            await _genericAccRepo.SaveChangesAsync();
            return true;
        }

        public async Task ChangePasswordNoCheckOldPassAsync(string email, string password)
        {
            await _accRepo.UpdatePasswordAsync(email, HashPassword(password));
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

        public async Task<AccountDTO> Add(AccountDTO accountDTO)
        {
            var exist = await _accRepo.ExistAccountAsync(accountDTO.Email);
            if (exist)
            {
                throw new Exception("Email already used in another account.");
            }
            var newAccount = _accMapper.ToModel(accountDTO);
            var password = RandomStringGenerator.Generate(12);
            newAccount.Password = HashPassword(password);
            await _genericAccRepo.AddAsync(newAccount);
            var account = await _accRepo.GetAccountFullInforAsync(accountDTO.Email);
            if (account != null)
            {
                await SMTPService.SendPasswordNewAccountEmail(newAccount.Email, password);
                return _accMapper.ToDTO(account);
            }
            throw new Exception("An error occurred while creating an account.");
        }

        public async Task<AccountDTO> Edit(AccountDTO accountDTO)
        {
            var account = await _genericAccRepo.GetByIdAsync(accountDTO.Id);
            if (account == null)
            {
                throw new Exception("Account does not exist.");
            }
            if (!account.Email.Equals(accountDTO.Email))
            {
                var exist = await _accRepo.ExistAccountAsync(accountDTO.Email);
                if (exist)
                {
                    throw new Exception("Email already used in another account.");
                }
            }
            account.StatusId = accountDTO.StatusId;
            account.Email = accountDTO.Email;
            account.RoleId = accountDTO.RoleId;
            await _genericAccRepo.UpdateAsync(account);
            return _accMapper.ToDTO(account);
        }

        public async Task Delete(long accountId)
        {
            var account = await _genericAccRepo.GetByIdAsync(accountId);
            if (account == null)
            {
                throw new Exception("Account doesn't exist.");
            }
            await _genericAccRepo.DeleteAsync(account);
        }

        public async Task<AccountDTO> Get(long accountId)
        {
            var account = await _genericAccRepo.GetByIdAsync(accountId);
            if (account == null)
            {
                throw new Exception("Account doesn't exist.");
            }
            return _accMapper.ToDTO(account);
        }

        public async Task<ICollection<AccountDTO>> GetAll()
        {
            var accounts = await _genericAccRepo.GetAllAsync();
            return _accMapper.TolistDTO(accounts);
        }

        public async Task<string[]> DeleteMany(long[] accountIds)
        {
            string[] messages = new string[accountIds.Length];
            foreach (var id in accountIds)
            {
                var account = await _genericAccRepo.GetByIdAsync(id);
                if (account == null)
                {
                    messages.Append($"Can't delete account id = {id}. Account doesn't exist.");
                }
                else
                {
                    await _genericAccRepo.DeleteAsync(account);
                    messages.Append($"Delete account id = {id} successfully.");
                }
            }
            return messages;
        }

        public async Task<(ICollection<AccountDTO>, int totalPages, int totalRecords)> GetPagesAsync(
            int pageNumber, int pageSize)
        {
            var (accounts, totalPage, totalRecords) = await _accRepo.GetPagedListAsync(pageNumber, pageSize);
            return (_accMapper.TolistDTO(accounts), totalPage, totalRecords);
        }

        public async Task<(ICollection<AccountDTO>, int totalPages, int totalRecords)> FilterAsync(
            AccountFilterDTO filterDTO)
        {
            if (filterDTO.Page < 1 || filterDTO.PageSize < 1)
            {
                throw new ArgumentException("Page and PageSize must be >= 1.");
            }
            var (accounts, totalPage, totalRecords) = await _accRepo.FilterAsync(filterDTO.Keyword,
                filterDTO.SortByEmail, filterDTO.FilterByStatus, filterDTO.FilterByRole,
                filterDTO.KeywordByEmployee, filterDTO.Page, filterDTO.PageSize);
            return (_accMapper.TolistDTO(accounts), totalPage, totalRecords);
        }
    }
}
