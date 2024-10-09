using PersonnelManagement.DTO;

namespace PersonnelManagement.Services
{
    public interface IProjectStatusService
    {
        Task<ProjectStatusDTO> Add(ProjectStatusDTO statusDTO);
        Task<ProjectStatusDTO> Edit(ProjectStatusDTO statusDTO);
        Task Delete(long statusId);
        Task<ProjectStatusDTO> Get(long statusId);
        Task<ICollection<ProjectStatusDTO>> GetAll();
    }
}
