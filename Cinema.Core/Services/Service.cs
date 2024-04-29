using Cinema.Core.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Core.Services
{
    public abstract class Service<T> : IService<T> where T : class
    {
        protected readonly IRepository<T> _repository;

        protected Service(IRepository<T> repository)
        {
            _repository = repository;
        }

        public virtual async Task<List<T>> GetAllAsync()
        {
            return await _repository.GetAll().ToListAsync();
        }

        public virtual async Task<T> FindByIdAsync(int id)
        {
            return await _repository.GetFirstOrDefaultAsync(m => m.Id == id);
        }

        public virtual async Task<T> Insert(T entity)
        {
            await _repository.Post(entity);
            // await _repository.SaveChangesAsync();
        }

        public virtual async Task<T> Update(T entity)
        {
            _repository.Update(entity);
            // await _repository.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(T entity, bool softDelete = false)
        {
            _repository.Delete(entity, softDelete);
            // await _repository.SaveChangesAsync();
        }

    }
}