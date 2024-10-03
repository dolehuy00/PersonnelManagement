using PersonnelManagement.Model;

namespace PersonnelManagement.Repositories
{
    public interface IAccountRepository
    {
        public Task<Account?> GetUserAsync(string email);
        public Task<Account?> GetAccountAsync(string email);
        public Task<bool> ExistAccountAsync(string email);
        public Task<bool> UpdatePasswordAsync(long accountId, string newPassword);
        public Task<bool> UpdatePasswordAsync(string email, string newPassword);
    }
}
