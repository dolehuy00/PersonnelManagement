namespace PersonnelManagement.Repositories
{
    public interface IGenericCurdRepository<T> where T : class
    {
        Task<ICollection<T>> GetAllAsync();
        Task<T?> GetByIdAsync(long id);
        Task<bool> ExistAsync(long id);
        bool Exist(long id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task DeleteAsync(long id);
        Task SaveChangesAsync();
    }
}
