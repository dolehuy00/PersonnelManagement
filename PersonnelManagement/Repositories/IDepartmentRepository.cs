using PersonnelManagement.DTO.Filter;
using PersonnelManagement.Model;

namespace PersonnelManagement.Repositories
{
    public interface IDepartmentRepository : IGenericRepository<Department>
    {
        Task<(ICollection<Department>, int, int)> FilterAsync(DepartmentFilterDTO departmentFilter);

        Task<Department> GetByIdIncludeLeaderAsync(long id);

    }
}
