using PersonnelManagement.DTO;

namespace PersonnelManagement.Services
{
    public interface IAccountService
    {
        public Task<AccountDTO?> ValidateUserAsync(string email, string password);
        public Task<bool> ChangePasswordAsync(long accountId, string currentPassword, string newPassword);
        public Task ChangePasswordNoCheckOldPassAsync(string email, string password);
        public Task<bool> ExistAccountAsync(string email);
        public Task<AccountDTO> Add(AccountDTO accountDTO);
        public Task<AccountDTO> Edit(AccountDTO accountDTO);
        public Task Delete(long accountId);
        public Task<AccountDTO> Get(long accountId);
        public Task<ICollection<AccountDTO>> GetAll();
        public Task<string[]> DeleteMany(long[] accountId);
        Task<(ICollection<AccountDTO>, int totalPages, int totalRecords)> GetPagedListWithTotalPagesAsync(
            int pageNumber, int pageSize);
        Task<(ICollection<AccountDTO>, int totalPages, int totalRecords)> FilterAsync(string? keyword,
            string? sortByEmail, int? filterByStatus, int? filterByRole, string? keywordByEmployee,
            int pageNumber, int pageSize);
    }
}
