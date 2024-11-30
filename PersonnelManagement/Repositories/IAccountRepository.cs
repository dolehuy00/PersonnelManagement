using PersonnelManagement.Model;

namespace PersonnelManagement.Repositories
{
    public interface IAccountRepository
    {
        Task<Account?> GetAccountFullInforAsync(string email);
        Task<(ICollection<Account>, int totalPages, int totalRecords)> FilterAsync(string? keyword,
            string? sortByEmail, string? filterByStatus, string? filterByRole, string? keywordByEmployee,
            int pageNumber, int pageSize);
    }
}
