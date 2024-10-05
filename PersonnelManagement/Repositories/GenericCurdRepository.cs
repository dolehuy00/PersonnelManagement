using Microsoft.EntityFrameworkCore;
using PersonnelManagement.Data;

namespace PersonnelManagement.Repositories
{
    public class GenericCurdRepository<T> : IGenericCurdRepository<T> where T : class
    {
        private readonly PersonnelDataContext _context;
        private readonly DbSet<T> _dbSet;

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
        public async Task<bool> ExistAccountAsync(long id)
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
    }

}
