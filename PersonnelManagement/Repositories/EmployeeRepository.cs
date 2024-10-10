using Microsoft.EntityFrameworkCore;
using PersonnelManagement.Data;
using PersonnelManagement.Model;

namespace PersonnelManagement.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly PersonnelDataContext _dataContext;

        public EmployeeRepository(PersonnelDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        async Task<Employee?> IEmployeeRepository.GetFullInforAsync(long id)
        {
            return await _dataContext.Employees
               .Include(e => e.Status)
               .Include(e => e.Department)
               .FirstOrDefaultAsync(e => e.Id == id);
        }

        async Task IEmployeeRepository.UpdateAsync(Employee employee)
        {
            var existingEntity = _dataContext.Employees.Local.FirstOrDefault(e => e.Id == employee.Id);
            if (existingEntity != null)
            {
                _dataContext.Entry(existingEntity).State = EntityState.Detached;
            }

            _dataContext.Employees.Update(employee);
            await _dataContext.SaveChangesAsync();
        }
        public async Task<(ICollection<Employee>, int totalPages, int totalRecords)> GetPagedListAsync(int pageNumber, int pageSize)
        {
            var skip = (pageNumber - 1) * pageSize;
            var totalRecords = await _dataContext.Employees.CountAsync();
            var totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
            var pagedList = await _dataContext.Employees
                .OrderBy(e => e.Id)
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync();

            return (pagedList, totalPages, totalRecords);
        }

        async Task<(ICollection<Employee>, int, int)> IEmployeeRepository.FilterAsync(string? nameOrId, string? address,
            DateTime? fromDoB, DateTime? toDoB, double? fromSalary, double? toSalary, string? position,
            DateTime? fromStartDate, DateTime? toStartDate, int? departmentId, string? status, string? sortBy,
            int page, int pageSize)
        {
            // Tim kiem theo ten hoac id, tim theo dia chi, loc theo khoang ngay sinh, loc theo khoang luong co ban
            // Loc theo vi tri, loc theo khoang ngay bat dau, loc theo phong ban, loc theo trang thai
            // Sap xep theo ten, sap xep theo ngay sinh, sap xep theo ngay bat dau
            var query = _dataContext.Employees.AsQueryable();

            // Tim kiem theo ten hoac id
            if (!string.IsNullOrEmpty(nameOrId))
            {
                query = query.Where(e => e.Fullname.Contains(nameOrId) || e.Id.ToString().Contains(nameOrId));
            }

            // Tim kiem theo dia chi
            if (!string.IsNullOrEmpty(address))
            {
                query = query.Where(e => e.Address.Contains(address));
            }

            // Loc theo khoang ngay sinh
            if (fromDoB != null)
            {
                query = query.Where(e => e.DateOfBirth >= fromDoB);
            }
            if (toDoB != null)
            {
                query = query.Where(e => e.DateOfBirth <= toDoB);
            }

            // Loc theo khoang luong co ban
            if (fromSalary.HasValue)
            {
                query = query.Where(e => e.BasicSalary >= fromSalary.Value);
            }
            if (toSalary.HasValue)
            {
                query = query.Where(e => e.BasicSalary <= toSalary.Value);
            }

            // Loc theo vi tri
            if (!string.IsNullOrEmpty(position))
            {
                query = query.Where(e => e.Position.Contains(position));
            }

            // Loc theo khoang ngay bat dau
            if (fromStartDate != null)
            {
                query = query.Where(e => e.StartDate >= fromStartDate);
            }
            if (toStartDate != null)
            {
                query = query.Where(e => e.StartDate <= toStartDate);
            }

            // Loc theo phong ban
            if (departmentId.HasValue)
            {
                query = departmentId < 1
                    ? query.Where(e => e.DepartmentId == null)
                    : query.Where(e => e.DepartmentId == departmentId.Value);
            }

            // Loc theo status
            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(e => e.Status.Contains(status));
            }

            // Sap xep theo ten, ngay sinh, ngay bat dau
            if (!string.IsNullOrEmpty(sortBy))
            {
                var sortBySplit = sortBy.Split(':');
                var sortField = sortBySplit[0].ToLower();
                var sortOrder = sortBySplit[1].ToLower();
                var sortFields = new Dictionary<string, Func<IQueryable<Employee>, IOrderedQueryable<Employee>>>
                {
                    { "fullname", q => sortOrder == "asc" ? q.OrderBy(e => e.Fullname) : q.OrderByDescending(e => e.Fullname) },
                    { "dateOfBirth", q => sortOrder == "asc" ? q.OrderBy(e => e.DateOfBirth) : q.OrderByDescending(e => e.DateOfBirth) },
                    { "startDate", q => sortOrder == "asc" ? q.OrderBy(e => e.StartDate) : q.OrderByDescending(e => e.StartDate) }
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
                                        "\tdateOfBirth:asc / dateOfBirth:dec\n" +
                                        "\tstartDate:asc / startDate:dec");
                }
            }
            else
            {
                query = query.OrderByDescending(e => e.Id);
            }

            // Phan trang
            var totalRecords = await query.CountAsync();
            var skip = (page - 1) * pageSize;
            var items = await query.Skip(skip).Take(pageSize).ToListAsync();
            var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
            return (items, totalPages, totalRecords);
        }
    }
}