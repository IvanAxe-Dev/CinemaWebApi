

namespace Cinema.Core.ServiceContracts
{
    public interface IService<T>
    {
        Task<List<T>> GetAllAsync();
        Task<T?> FindByIdAsync(Guid id);
        Task<T> Insert(T entity);
        Task<T> Update(T entity);
        Task DeleteAsync(T entity);
    }
}
