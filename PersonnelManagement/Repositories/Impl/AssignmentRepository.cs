using Microsoft.EntityFrameworkCore;
using PersonnelManagement.Data;
using PersonnelManagement.Model;

namespace PersonnelManagement.Repositories.Impl
{
    public class AssignmentRepository : IAssignmentRepository
    {
        private readonly PersonnelDataContext _dataContext;

        public AssignmentRepository(PersonnelDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<(ICollection<Assignment>, int, int)> FilterAsync(
            string? sortBy, string? status, long? responsiblePesonId, long? projectId,
            long? departmentId, long? deptAssignmentId, int page, int pageSize)
        {
            // Loc theo status, xep theo name, sap xep theo level, loc theo employee
            // Loc theo project id, loc theo department id.
            var query = _dataContext.Assignments
                .Include(a => a.DeptAssignment)
                    .ThenInclude(dept => dept.Project)
                .Include(a => a.ResponsiblePeson)
                .AsQueryable();

            // Lọc theo status
            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(a => a.Status.Contains(status));
            }

            // Lọc theo id employee
            if (responsiblePesonId.HasValue)
            {
                query = query.Where(a => a.ResponsiblePesonId == responsiblePesonId.Value);
            }

            // Lọc theo id employee
            if (deptAssignmentId.HasValue)
            {
                query = query.Where(a => a.DeptAssignmentId == deptAssignmentId.Value);
            }

            // Lọc theo id project
            if (projectId.HasValue)
            {
                query = query.Where(a => a.DeptAssignment.ProjectId == projectId.Value);
            }

            // Lọc theo id department
            if (departmentId.HasValue)
            {
                query = query.Where(a => a.DeptAssignment.DepartmentId == departmentId.Value);
            }

            // Sắp xếp theo name, level
            if (!string.IsNullOrEmpty(sortBy))
            {
                var sortBySplit = sortBy.Split(':');
                var sortField = sortBySplit[0].ToLower();
                var sortOrder = sortBySplit[1].ToLower();
                var sortFields = new Dictionary<string, Func<IQueryable<Assignment>, IOrderedQueryable<Assignment>>>
                {
                    { "fullname", q => sortOrder == "asc" ? q.OrderBy(e => e.Name) : q.OrderByDescending(e => e.Name) },
                    { "priotitylevel", q => sortOrder == "asc" ? q.OrderBy(e => e.PriotityLevel) : q.OrderByDescending(e => e.PriotityLevel) }
                };

                if (sortFields.ContainsKey(sortField))
                {
                    query = sortFields[sortField](query);
                }
                else
                {
                    throw new Exception("Invalid sort field.\n" +
                                        "We support:\n" +
                                        "\tfullname:asc / fullname:dec\n" +
                                        "\tpriotityLevel:asc / dateOfBirth:dec");
                }
            }
            else
            {
                query = query.OrderByDescending(e => e.Id);
            }

            var totalRecords = await query.CountAsync();
            var skip = (page - 1) * pageSize;
            var items = await query
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync();
            var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
            return (items, totalPages, totalRecords);
        }

        public async Task<(ICollection<Assignment>, int, int)> GetPagedListByEmployeeAsync(int pageNumber, int pageSize, long employeeId)
        {
            var query = _dataContext.Assignments
                .Include(s => s.ResponsiblePeson)
                .Where(s => s.ResponsiblePesonId == employeeId)
                .AsQueryable();

            var skip = (pageNumber - 1) * pageSize;
            var totalRecords = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
            var pagedList = await query
                .OrderBy(s => s.Id)
                .Skip(skip)
                .Take(pageSize)
                .Include(s => s.ResponsiblePeson)
                .ToListAsync();

            return (pagedList, totalPages, totalRecords);
        }
    }
}
