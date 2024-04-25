using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Infrastructure.Repositories
{
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
        Task UpdateMany(Expression<Func<T, bool>> predicate,
            Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> setPropertyCalls);

        void Delete(T entity);
        void DeleteAll(IEnumerable<T> entities);

        void Patch(T entity);

        Task SaveChangesAsync();
    }
}
