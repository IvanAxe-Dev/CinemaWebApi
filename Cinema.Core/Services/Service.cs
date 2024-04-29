using Cinema.Core.Domain.RepositoryContracts;
using Cinema.Core.Domain.Entities;
using Cinema.Core.ServiceContracts;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Core.Services
{
    public class Service<T> : IService<T> where T : class, IBaseEntity
    {
        private readonly IRepository<T> _repository;

        public Service(IRepository<T> repository)
        {
            _repository = repository;
        }

        public Task<List<T>> GetAllAsync()
        {
            return _repository.GetAll().ToListAsync();
        }

        public Task<T?> FindByIdAsync(Guid id)
        {
            return _repository.GetFirstOrDefaultAsync(entity => entity.Id == id);
        }

        public async Task<T> Insert(T entity)
        {
            await _repository.Post(entity);
            await _repository.SaveChangesAsync();
            return entity;
        }

        public async Task<T> Update(T entity)
        {
            _repository.Update(entity);
            await _repository.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            _repository.Delete(entity);
            await _repository.SaveChangesAsync();
        }
    }
}