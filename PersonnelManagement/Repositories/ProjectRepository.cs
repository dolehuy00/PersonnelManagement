using Microsoft.EntityFrameworkCore;
using PersonnelManagement.Data;
using PersonnelManagement.DTO;
using PersonnelManagement.DTO.Filter;
using PersonnelManagement.Model;

namespace PersonnelManagement.Repositories
{
    public class ProjectRepository : GenericCurdRepository<Project>, IProjectRepository
    {
        public ProjectRepository(PersonnelDataContext context) : base(context){}

        async Task<(ICollection<Project>, int, int)> IProjectRepository.FilterAsync(ProjectFilterDTO projectFilter)
        {
            var query = this._context.Projects.AsQueryable();

            //Name or Id
            if (!String.IsNullOrEmpty(projectFilter.Name) || projectFilter.Id != null) {
                query = query.Where(e =>
                (string.IsNullOrEmpty(projectFilter.Name) || e.Name.Contains(projectFilter.Name)) &&
                (projectFilter.Id == null || e.Id == projectFilter.Id)
                );
            }

            //Duration
            // Date filter based on StartTime and Duration
            if (projectFilter.StartDate.HasValue && projectFilter.EndDate.HasValue)
            {
                // Nếu có cả startTime và Duration
                query = query.Where(e => (e.StartDate >= projectFilter.StartDate && e.StartDate <= projectFilter.EndDate));
            }
            else if (projectFilter.StartDate.HasValue)
            {
                // Nếu chỉ có startTime
                query = query.Where(e => e.StartDate >= projectFilter.StartDate);
            }
            else if (projectFilter.EndDate.HasValue)
            {
                // Nếu chỉ có Duration
                query = query.Where(e => e.StartDate <= projectFilter.EndDate);
            }

            //Status
            if (!String.IsNullOrEmpty(projectFilter.Status)) {
                query = query.Where(e=> (e.Status == projectFilter.Status));
            }               

            //Sorting
            if (!String.IsNullOrEmpty(projectFilter.SortBy)) {
                var sortBySplit = projectFilter.SortBy.Split(':');
                var sortField = sortBySplit[0].ToLower();
                var sortOrder = sortBySplit[1].ToLower();
                if (sortField == "name" || sortField == "id")
                    query = ApplySorting(query, sortField, sortOrder);
            } else {
                query = query.OrderByDescending(e => e.Id);
            }

            // Phan trang
            return await ApplyPaging(query, projectFilter.Page, projectFilter.PageSize);
        }
    }
}
