using System.Linq.Expressions;

namespace PersonnelManagement.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<ICollection<T>> GetAllAsync();
        Task<T?> GetByIdAsync(long id);
        Task<bool> ExistAsync(long id);
        bool Exist(long id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task DeleteAsync(long id);
        Task SaveChangesAsync();
        Task<ICollection<T>> FindListAsync(
            Expression<Func<T, bool>>? predicate,
            params Expression<Func<T, object>>[] includes);
        Task<T?> FindOneAsync(
            Expression<Func<T, bool>>? predicate,
            params Expression<Func<T, object>>[] includes);
    }
}
