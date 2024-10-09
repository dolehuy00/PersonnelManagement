using PersonnelManagement.DTO;

namespace PersonnelManagement.Services
{
    public interface IDepartmentStatusService
    {
        Task<DepartmentStatusDTO> Add(DepartmentStatusDTO statusDTO);
        Task<DepartmentStatusDTO> Edit(DepartmentStatusDTO statusDTO);
        Task Delete(long statusId);
        Task<DepartmentStatusDTO> Get(long statusId);
        Task<ICollection<DepartmentStatusDTO>> GetAll();
    }
}
