using Microsoft.EntityFrameworkCore;
using PersonnelManagement.Data;
using System.Linq.Expressions;

namespace PersonnelManagement.Repositories
{
    public class GenericCurdRepository<T> : IGenericCurdRepository<T> where T : class
    {
        protected readonly PersonnelDataContext _context;
        protected readonly DbSet<T> _dbSet;

        public GenericCurdRepository(PersonnelDataContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<ICollection<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(long id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<bool> ExistAsync(long id)
        {
            return await _dbSet.FindAsync(id) != null;
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            await SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            await SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public static IQueryable<T> ApplySorting<T>(IQueryable<T> query, string sortField, string sortOrder)
        {
            if (string.IsNullOrEmpty(sortField)) return query;

            // Lấy thông tin của trường qua reflection
            var parameter = Expression.Parameter(typeof(T), "e");
            var property = Expression.Property(parameter, sortField);
            var lambda = Expression.Lambda(property, parameter);

            // Sắp xếp theo thứ tự tăng dần hay giảm dần
            string methodName = sortOrder == "asc" ? "OrderBy" : "OrderByDescending";

            // Gọi hàm LINQ OrderBy hoặc OrderByDescending thông qua reflection
            var resultExpression = Expression.Call(
                typeof(Queryable),
                methodName,
                new Type[] { query.ElementType, property.Type },
                query.Expression,
                Expression.Quote(lambda));

            return query.Provider.CreateQuery<T>(resultExpression);
        }

        public static async Task<(List<T> Items, int TotalPages, int TotalRecords)> ApplyPaging<T>(
            IQueryable<T> query,
            int page,
            int pageSize)
        {
            // Tính tổng số bản ghi
            var totalRecords = await query.CountAsync();

            // Phân trang
            var skip = (page - 1) * pageSize;
            var items = await query.Skip(skip).Take(pageSize).ToListAsync();

            // Tính tổng số trang
            var totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            return (items, totalPages, totalRecords);
        }


    }

}
