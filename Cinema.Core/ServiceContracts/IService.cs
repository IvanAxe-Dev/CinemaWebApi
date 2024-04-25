using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Core.ServiceContracts
{
    public interface IService<T>
    {
        Task<List<T>> GetAllAsync();
        Task FindByIdAsync(int Id);
        Task<T> Insert(T entity);
        Task<T> Update(T entity);
        Task DeleteAsync(T entity, bool softDelete = false);
    }
}
