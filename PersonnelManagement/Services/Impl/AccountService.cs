﻿using MovieAppApi.Service;
using PersonnelManagement.DTO;
using PersonnelManagement.DTO.Filter;
using PersonnelManagement.Enum;
using PersonnelManagement.Mappers;
using PersonnelManagement.Model;
using PersonnelManagement.Repositories;
using System.Linq.Expressions;

namespace PersonnelManagement.Services.Impl
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accRepo;
        private readonly AccountMapper _accMapper;

        public AccountService(IAccountRepository accountRepository)
        {
            _accRepo = accountRepository;
            _accMapper = new AccountMapper();
        }

        public async Task<AccountDTO?> ValidateUserAsync(string email, string password)
        {
            var account = await _accRepo.GetAccountFullInforAsync(email);
            if (account != null && VerifyPassword(password, account.Password) && account.Status == "Active")
            {
                return _accMapper.ToDTO(account);
            }
            return null;
        }

        public async Task<bool> ChangePasswordAsync(long accountId, string currentPassword, string newPassword)
        {
            var account = await _accRepo.GetByIdAsync(accountId);
            if (account == null || !VerifyPassword(currentPassword, account.Password))
            {
                return false;
            }
            account.Password = HashPassword(newPassword);
            await _accRepo.SaveChangesAsync();
            return true;
        }

        public async Task ChangePasswordNoCheckOldPassAsync(string email, string password)
        {
            Expression<Func<Account, bool>> expression = acc => acc.Email.Equals(email);
            var account = await _accRepo.FindOneAsync(expression) ?? throw new Exception("Account doesn't exist.");
            account.Password = HashPassword(password);
            await _accRepo.SaveChangesAsync();
        }

        public async Task<bool> ExistAccountAsync(string email)
        {
            Expression<Func<Account, bool>> expression = acc => acc.Email.Equals(email);
            return await _accRepo.FindOneAsync(expression) != null;
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
            Expression<Func<Account, bool>> expressionExist = acc => acc.Email.Equals(accountDTO.Email);
            var exist = _accRepo.FindOneAsync(expressionExist) == null;
            if (exist)
            {
                throw new Exception("Email already used in another account.");
            }
            var newAccount = _accMapper.ToModel(accountDTO);
            var password = RandomStringGenerator.Generate(12);
            newAccount.Password = HashPassword(password);
            await _accRepo.AddAsync(newAccount);
            await SMTPService.SendPasswordNewAccountEmail(newAccount.Email, password);
            return _accMapper.ToDTO(newAccount);
            throw new Exception("An error occurred while creating an account.");
        }

        public async Task<AccountDTO> Edit(AccountDTO accountDTO)
        {
            var account = await _accRepo.GetByIdAsync(accountDTO.Id) ?? throw new Exception("Account does not exist.");
            if (!account.Email.Equals(accountDTO.Email))
            {
                Expression<Func<Account, bool>> expression = acc => acc.Email.Equals(accountDTO.Email);
                var exist = _accRepo.FindOneAsync(expression) != null;
                if (exist)
                {
                    throw new Exception("Email already used in another account.");
                }
            }
            account.EmployeeId = accountDTO.EmployeeId;
            account.RoleId = accountDTO.RoleId;
            await _accRepo.UpdateAsync(account);
            return _accMapper.ToDTO(account);
        }

        public async Task Delete(long accountId)
        {
            var account = await _accRepo.GetByIdAsync(accountId) ?? throw new Exception("Account doesn't exist.");
            await _accRepo.DeleteAsync(account);
        }

        public async Task<AccountDTO> Get(long accountId)
        {
            Expression<Func<Account, bool>> predicate = a => a.Id == accountId;
            Expression<Func<Account, object>>[] includes = [a => a.Role, a => a.Employee];
            var account = await _accRepo.FindOneAsync(predicate, includes);
            return account == null ? throw new Exception("Account doesn't exist.") : _accMapper.ToDTO(account);
        }

        public async Task<ICollection<AccountDTO>> GetAll()
        {
            var accounts = await _accRepo.GetAllAsync();
            return _accMapper.TolistDTO(accounts);
        }

        public async Task<string[]> DeleteMany(long[] accountIds)
        {
            string[] messages = new string[accountIds.Length];
            foreach (var id in accountIds)
            {
                var account = await _accRepo.GetByIdAsync(id);
                if (account == null)
                {
                    messages.Append($"Can't delete account id = {id}. Account doesn't exist.");
                }
                else
                {
                    await _accRepo.DeleteAsync(account);
                    messages.Append($"Delete account id = {id} successfully.");
                }
            }
            return messages;
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

        public async Task<bool> Lock(long id)
        {
            var empl = await _accRepo.GetByIdAsync(id) ?? throw new Exception("Account does not exist.");
            empl.Status = Status.Lock;
            await _accRepo.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UnLock(long id)
        {
            var empl = await _accRepo.GetByIdAsync(id) ?? throw new Exception("Account does not exist.");
            empl.Status = Status.Active;
            await _accRepo.SaveChangesAsync();
            return true;
        }

    }
}
