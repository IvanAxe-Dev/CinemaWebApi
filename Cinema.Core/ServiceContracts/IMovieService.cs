using Cinema.Core.Domain.Entities;
using Cinema.Core.Domain.IdentityEntities;
using Cinema.Core.DTO;
using System.Security.Claims;

namespace Cinema.Core.ServiceContracts
{
    public interface IMovieService : IService<Movie>
    { 
        Task<MovieResponse?> GetMovieWithCategoriesById(Guid movieId, ClaimsPrincipal? user);
        Task<List<MovieResponse>> GetAllMoviesWithCategories();
        Task<List<MovieResponse>> TakeNLatestMovies(int? moviesToTake);
        Task<List<MovieResponse>> GetFilteredMovies(GetMoviesQuery request);
        Task RateMovie(Guid movieId, ClaimsPrincipal user, int rating);
        Task<List<MovieResponse>> GetRecommendedMovies(ApplicationUser user);
        Task UploadImageToMovie(Guid id, string image);
        Task UpdateRecentlyWatchedCategoriesAsync(ClaimsPrincipal user, IEnumerable<CategoryResponse> categories);
    }
}