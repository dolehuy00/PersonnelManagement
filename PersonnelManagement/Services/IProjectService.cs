using PersonnelManagement.DTO.Filter;
using PersonnelManagement.DTO;

namespace PersonnelManagement.Services
{
    public interface IProjectService
    {
        Task<(ICollection<ProjectDTO>, int totalPages, int totalRecords)> FilterAsync(
            ProjectFilterDTO projectFilter);

        Task<ProjectDTO> Add(ProjectDTO projectDTO);
        Task<ProjectDTO> Edit(ProjectDTO projectDTO);
        Task Delete(long projectId);
        Task<ProjectDTO?> Get(long projectIds);
        Task<ICollection<ProjectDTO>> GetAll();
        Task<string[]> DeleteMany(long[] projectId);
    }
}
