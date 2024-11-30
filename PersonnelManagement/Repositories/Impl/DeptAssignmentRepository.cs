using Microsoft.EntityFrameworkCore;
using PersonnelManagement.Data;
using PersonnelManagement.DTO;
using PersonnelManagement.DTO.Filter;
using PersonnelManagement.Model;

namespace PersonnelManagement.Repositories.Impl
{
    public class DeptAssignmentRepository : GenericRepository<DeptAssignment>, IDeptAssignmentRepository
    {
        public DeptAssignmentRepository(PersonnelDataContext context) : base(context) { }

        async Task<(ICollection<DeptAssignment>, int, int)> IDeptAssignmentRepository.FilterAsync(DeptAssignmentFilterDTO deptAssignmentFilter)
        {
            var query = _context.DeptAssignments.AsQueryable();

            // Id
            if (deptAssignmentFilter.Id != null)
            {
                query = query.Where(e => deptAssignmentFilter.Id == null || e.Id == deptAssignmentFilter.Id);
            }
            if (deptAssignmentFilter.departmentId != null)
            {
                query = query.Where(e => e.Department.Id == deptAssignmentFilter.departmentId);
            }
            if (deptAssignmentFilter.projectId != null)
            {
                query = query.Where(e => e.Project.Id == deptAssignmentFilter.projectId);
            }

            //Sorting
            if (!string.IsNullOrEmpty(deptAssignmentFilter.SortBy))
            {
                var sortBySplit = deptAssignmentFilter.SortBy.Split(':');
                var sortField = sortBySplit[0].ToLower();
                var sortOrder = sortBySplit[1].ToLower();
                if (sortField == "name" || sortField == "id")
                    query = ApplySorting(query, sortField, sortOrder);
            }
            else
            {
                query = query.OrderByDescending(e => e.Id);
            }

            // Phan trang
            return await ApplyPaging(query, deptAssignmentFilter.Page, deptAssignmentFilter.PageSize);
        }
    }
}
