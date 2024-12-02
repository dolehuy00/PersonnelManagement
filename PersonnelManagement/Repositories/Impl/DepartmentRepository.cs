using Microsoft.EntityFrameworkCore;
using PersonnelManagement.Data;
using PersonnelManagement.DTO;
using PersonnelManagement.DTO.Filter;
using PersonnelManagement.Model;

namespace PersonnelManagement.Repositories.Impl
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(PersonnelDataContext context) : base(context) { }

        async Task<(ICollection<Department>, int, int)> IDepartmentRepository.FilterAsync(DepartmentFilterDTO departmentFilter)
        {
            var query = _context.Departments.AsQueryable();

            //Name or Id
            if (!string.IsNullOrEmpty(departmentFilter.Name) || departmentFilter.Id != null)
            {
                query = query.Where(e => (string.IsNullOrEmpty(departmentFilter.Name) || e.Name.Contains(departmentFilter.Name)) &&
                (departmentFilter.Id == null || e.Id == departmentFilter.Id)
                );
            }

            //Status
            if (!string.IsNullOrEmpty(departmentFilter.Status))
            {
                query = query.Where(e => e.Status == departmentFilter.Status);
            }

            //Sorting
            if (!string.IsNullOrEmpty(departmentFilter.SortBy))
            {
                var sortBySplit = departmentFilter.SortBy.Split(':');
                var sortField = sortBySplit[0].ToLower();
                var sortOrder = sortBySplit[1].ToLower();
                if (sortField == "name" || sortField == "id")
                    query = ApplySorting(query, sortField, sortOrder);
            }
            else
            {
                query = query.OrderByDescending(e => e.Id);
            }

            query = query.Include(d => d.Leader);

            // Phan trang
            return await ApplyPaging(query, departmentFilter.Page, departmentFilter.PageSize);
        }


        public async Task<Department> GetByIdIncludeLeaderAsync(long id)
        {
            return await _context.Departments
                .Include(d => d.Leader)  // Bao gồm Leader khi truy vấn
                .FirstOrDefaultAsync(d => d.Id == id); // Hoặc sử dụng FindAsync tùy theo cách bạn tổ chức truy vấn
        }
    }
}
