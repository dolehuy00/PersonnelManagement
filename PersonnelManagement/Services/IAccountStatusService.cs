using PersonnelManagement.DTO;

namespace PersonnelManagement.Services
{
    public interface IAccountStatusService
    {
        Task<AccountStatusDTO> Add(AccountStatusDTO accountStatusDTO);
        Task<AccountStatusDTO> Edit(AccountStatusDTO accountStatusDTO);
        Task Delete(long accountId);
        Task<AccountStatusDTO> Get(long statusId);
        Task<ICollection<AccountStatusDTO>> GetAll();
    }
}
