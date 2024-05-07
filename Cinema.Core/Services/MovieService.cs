using Cinema.Core.Domain.Entities;
using Cinema.Core.Domain.RepositoryContracts;
using Cinema.Core.ServiceContracts;
using MapsterMapper;

namespace Cinema.Core.Services
{
    public class MovieService : Service<Movie>, IMovieService
    {
        private readonly IMapper _mapster;

        public MovieService(IMovieRepository repository, IMapper mapster) : base(repository)
        {
            _mapster = mapster;
        }
    }
}