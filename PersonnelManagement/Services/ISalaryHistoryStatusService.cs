using PersonnelManagement.DTO;

namespace PersonnelManagement.Services
{
    public interface ISalaryHistoryStatusService
    {
        Task<SalaryHistoryStatusDTO> Add(SalaryHistoryStatusDTO statusDTO);
        Task<SalaryHistoryStatusDTO> Edit(SalaryHistoryStatusDTO statusDTO);
        Task Delete(long statusId);
        Task<SalaryHistoryStatusDTO> Get(long statusId);
        Task<ICollection<SalaryHistoryStatusDTO>> GetAll();
    }
}
