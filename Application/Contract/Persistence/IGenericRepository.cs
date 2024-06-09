namespace Application.Contract.Persistence
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<T> DeleteAsync(T entity);
    }
}
