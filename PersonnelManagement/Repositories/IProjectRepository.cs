using PersonnelManagement.DTO;
using PersonnelManagement.DTO.Filter;
using PersonnelManagement.Model;

namespace PersonnelManagement.Repositories
{
    public interface IProjectRepository : IGenericRepository<Project>
    {
        Task<(ICollection<Project>, int, int)> FilterAsync(ProjectFilterDTO projectFilter);

    }
}
