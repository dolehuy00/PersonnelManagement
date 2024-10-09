using PersonnelManagement.DTO;

namespace PersonnelManagement.Services
{
    public interface IEmployeeStatusService
    {
        Task<EmployeeStatusDTO> Add(EmployeeStatusDTO statusDTO);
        Task<EmployeeStatusDTO> Edit(EmployeeStatusDTO statusDTO);
        Task Delete(long statusId);
        Task<EmployeeStatusDTO> Get(long statusId);
        Task<ICollection<EmployeeStatusDTO>> GetAll();
    }
}
