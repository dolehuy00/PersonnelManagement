﻿using PersonnelManagement.DTO;

namespace PersonnelManagement.Services
{
    public interface IAccountService
    {
        public Task<AccountDTO?> ValidateUserAsync(string email, string password);
        public Task<bool> ChangePasswordAsync(long accountId, string currentPassword, string newPassword);
        public Task<bool> ChangePasswordAsync(string email, string currentPassword, string newPassword);
        public Task<bool> ExistAccountAsync(string email);
        public Task<AccountDTO> Add(AccountDTO account);
    }
}
