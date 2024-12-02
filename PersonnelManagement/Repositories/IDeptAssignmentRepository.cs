using PersonnelManagement.DTO;
using PersonnelManagement.DTO.Filter;
using PersonnelManagement.Model;

namespace PersonnelManagement.Repositories
{
    public interface IDeptAssignmentRepository : IGenericRepository<DeptAssignment>
    {
        Task<(ICollection<DeptAssignment>, int, int)> FilterAsync(DeptAssignmentFilterDTO projectFilter);
        Task DeleteByProjectIdAsync(long projectId);
    }
}
