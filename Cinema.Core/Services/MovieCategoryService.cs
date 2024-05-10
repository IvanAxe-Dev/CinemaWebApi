using Cinema.Core.Domain.Entities;
using Cinema.Core.Domain.RepositoryContracts;
using Cinema.Core.ServiceContracts;

namespace Cinema.Core.Services;

public class MovieCategoryService : Service<MovieCategory>, IMovieCategoryService
{
    private readonly IMovieCategoryRepository _repository;

    public MovieCategoryService(IMovieCategoryRepository repository) : base(repository)
    {
        _repository = repository;
    }
}