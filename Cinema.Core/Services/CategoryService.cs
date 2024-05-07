using Cinema.Core.Domain.Entities;
using Cinema.Core.Domain.RepositoryContracts;
using Cinema.Core.ServiceContracts;
using MapsterMapper;

namespace Cinema.Core.Services
{
    public class CategoryService : Service<Category>, ICategoryService
    {
        private readonly IMapper _mapster;

        public CategoryService(ICategoryRepository repository, IMapper mapster) : base(repository)
        {
            _mapster = mapster;
        }
    }
}
