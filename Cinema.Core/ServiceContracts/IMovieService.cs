using Cinema.Core.Domain.Entities;
using Cinema.Core.DTO;

namespace Cinema.Core.ServiceContracts
{
    public interface IMovieService : IService<Movie>
    { 
        Task<MovieResponse?> GetMovieWithCategoriesById(Guid movieId);
        Task<List<MovieResponse>> GetAllMoviesWithCategories();

    }
}