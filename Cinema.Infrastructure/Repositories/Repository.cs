using Cinema.Core.Domain.Entities;
using Microsoft.Data.SqlClient;
using Cinema.Core.Domain.RepositoryContracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using Cinema.Core.Domain.Entities;

namespace Cinema.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class, IBaseEntity
    {
        protected readonly SqlcinemadbContext dataContext;
        protected readonly DbSet<T> dbSet;

        protected virtual IQueryable<T> PrepareDbSet()
        {
            return dbSet;
        }

        public Repository(SqlcinemadbContext dataContext)
        {
            this.dataContext = dataContext ?? throw new ArgumentNullException();
            dbSet = dataContext.Set<T>();
        }

        public virtual async Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>>? predicate = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null)
        {
            var preparedDbSet = PrepareDbSet();

            if (include != null)
            {
                preparedDbSet = include(preparedDbSet);
            }

            if (predicate is null)
                return await preparedDbSet.FirstOrDefaultAsync();

            return await preparedDbSet.FirstOrDefaultAsync(predicate);
        }

        public virtual async Task<T> GetFirstAsync(Expression<Func<T, bool>>? predicate = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null)
        {
            var preparedDbSet = PrepareDbSet();

            if (include != null)
            {
                preparedDbSet = include(preparedDbSet);
            }

            if (predicate is null)
                return await preparedDbSet.FirstAsync();

            return await preparedDbSet.FirstAsync(predicate);
        }

        public virtual IQueryable<T> GetWhere(Expression<Func<T, bool>>? predicate,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            bool ignoreDbSet = false)
        {
            if (predicate is null)
                throw new ArgumentNullException(nameof(predicate));

            IQueryable<T> preparedDbSet = dataContext.Set<T>();
            if (!ignoreDbSet)
                preparedDbSet = PrepareDbSet();

            if (include != null)
            {
                preparedDbSet = include(preparedDbSet);
            }

            return preparedDbSet.Where(predicate);
        }

        public virtual IQueryable<T> GetAll(Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null)
        {
            var preparedDbSet = PrepareDbSet();

            if (include != null)
            {
                preparedDbSet = include(preparedDbSet);
            }

            return preparedDbSet;
        }

        public async Task Post(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            await dbSet.AddAsync(entity);
        }

        public virtual void Update(T entity)
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity));

            dbSet.Update(entity);
        }

        public virtual async Task UpdateMany(Expression<Func<T, bool>> predicate,
            Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> setPropertyCalls)
        {
            var preparedDbSet = PrepareDbSet();

            await preparedDbSet.Where(predicate).ExecuteUpdateAsync(setPropertyCalls);
        }

        public virtual void Delete(T entity)
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity));

            var entry = dataContext.Entry(entity);
            entry.State = EntityState.Deleted;
            dbSet.Remove(entity);
        }

        public virtual void DeleteAll(IEnumerable<T> entities)
        {
            if (entities is null)
                throw new ArgumentNullException(nameof(entities));

            foreach (var entity in entities)
            {
                var entry = dataContext.Entry<T>(entity);
                entry.State = EntityState.Deleted;
                dbSet.Remove(entity);
            }
        }

        public virtual void Patch(T entity)
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity));

            dbSet.Update(entity);
        }

        public async Task SaveChangesAsync()
        {
            await dataContext.SaveChangesAsync();
        }
    }
}
