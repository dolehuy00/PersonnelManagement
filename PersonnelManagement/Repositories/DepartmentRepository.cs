using Microsoft.EntityFrameworkCore;
using PersonnelManagement.Data;
using PersonnelManagement.DTO;
using PersonnelManagement.DTO.Filter;
using PersonnelManagement.Model;

namespace PersonnelManagement.Repositories
{
    public class DepartmentRepository : GenericCurdRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(PersonnelDataContext context) : base(context){}

        async Task<(ICollection<Department>, int, int)> IDepartmentRepository.FilterAsync(DepartmentFilterDTO departmentFilter)
        {
            var query = this._context.Departments.AsQueryable();

            //Name or Id
            if (!String.IsNullOrEmpty(departmentFilter.Name) || departmentFilter.Id != null) {
                query = query.Where(e =>
                (string.IsNullOrEmpty(departmentFilter.Name) || e.Name.Contains(departmentFilter.Name)) &&
                (departmentFilter.Id == null || e.Id == departmentFilter.Id)
                );
            }

            //Status
            if (!String.IsNullOrEmpty(departmentFilter.Status)) {
                query = query.Where(e=> (e.Status == departmentFilter.Status));
            }

            //Sorting
            if (!String.IsNullOrEmpty(departmentFilter.SortBy)) {
                var sortBySplit = departmentFilter.SortBy.Split(':');
                var sortField = sortBySplit[0].ToLower();
                var sortOrder = sortBySplit[1].ToLower();
                if (sortField == "name" || sortField == "id")
                    query = ApplySorting(query, sortField, sortOrder);
            } else {
                query = query.OrderByDescending(e => e.Id);
            }

            // Phan trang
            return await ApplyPaging(query, departmentFilter.Page, departmentFilter.PageSize);
        }
    }
}
