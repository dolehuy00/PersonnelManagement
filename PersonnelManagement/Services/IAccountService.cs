using PersonnelManagement.DTO;
using PersonnelManagement.DTO.Filter;

namespace PersonnelManagement.Services
{
    public interface IAccountService
    {
        Task<AccountDTO?> ValidateUserAsync(string email, string password);
        Task<bool> ChangePasswordAsync(long accountId, string currentPassword, string newPassword);
        Task ChangePasswordNoCheckOldPassAsync(string email, string password);
        Task<bool> ExistAccountAsync(string email);
        Task<AccountDTO> Add(AccountDTO accountDTO);
        Task<AccountDTO> Edit(AccountDTO accountDTO);
        Task Delete(long accountId);
        Task<AccountDTO> Get(long accountId);
        Task<ICollection<AccountDTO>> GetAll();
        Task<string[]> DeleteMany(long[] accountId);
        Task<(ICollection<AccountDTO>, int totalPages, int totalRecords)> GetPagesAsync(
            int pageNumber, int pageSize);
        Task<(ICollection<AccountDTO>, int totalPages, int totalRecords)> FilterAsync(
            AccountFilterDTO filterDTO);
        Task<bool> Lock(long id);
        Task<bool> UnLock(long id);
    }
}
