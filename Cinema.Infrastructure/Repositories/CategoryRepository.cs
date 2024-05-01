using Cinema.Core.Domain.Entities;
using Cinema.Core.Domain.RepositoryContracts;

namespace Cinema.Infrastructure.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(SqlcinemadbContext dataContext) : base(dataContext)
        {

        }
    }
}
