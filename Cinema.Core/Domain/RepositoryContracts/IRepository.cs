using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;


namespace Cinema.Core.Domain.RepositoryContracts;

public interface IRepository<T>
{
    Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>>? predicate = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);

    Task<T> GetFirstAsync(Expression<Func<T, bool>>? predicate = null,
    Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);

    IQueryable<T> GetWhere(Expression<Func<T, bool>>? predicate = null,
        Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
        bool ignorePrepareDbSet = false);

    IQueryable<T> GetAll(Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);

    Task Post(T entity);

    void Update(T entity);

    void Delete(T entity);
    void DeleteAll(IEnumerable<T> entities);

    void Patch(T entity);

    Task SaveChangesAsync();
}
