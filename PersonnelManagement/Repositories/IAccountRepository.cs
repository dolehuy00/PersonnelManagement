using PersonnelManagement.Model;

namespace PersonnelManagement.Repositories
{
    public interface IAccountRepository
    {
        public Task<Account?> GetAccountFullInforAsync(string email);
        public Task<Account?> GetAccountAsync(string email);
        public Task<bool> ExistAccountAsync(string email);
        public Task<bool> UpdatePasswordAsync(long accountId, string newPassword);
        public Task UpdatePasswordAsync(string email, string newPassword);
        Task<(ICollection<Account>, int totalPages, int totalRecords)> GetPagedListAsync(int pageNumber, int pageSize);
        Task SaveChangesAsync();
    }
}
