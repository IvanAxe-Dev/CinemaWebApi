using Cinema.Core.Domain.Entities;
using Cinema.Core.Domain.RepositoryContracts;
using Cinema.Core.DTO;
using Cinema.Core.ServiceContracts;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Core.Services
{
    public class MovieService : Service<Movie>, IMovieService
    {
        private readonly IMapper _mapster;
        private readonly IMovieRepository _movieRepository;
        private readonly IMovieCategoryRepository _movieCategoryRepository;
        private readonly ITicketRepository _ticketRepository;

        public MovieService(IMovieRepository repository, IMapper mapster, IMovieCategoryRepository movieCategoryRepository,
            ITicketRepository ticketRepository) : base(repository)
        {
            _mapster = mapster;
            _movieRepository = repository;
            _movieCategoryRepository = movieCategoryRepository;
            _ticketRepository = ticketRepository;
        }

        public async Task<MovieResponse?> GetMovieWithCategoriesById(Guid movieId)
        {
            Movie? movie = await FindByIdAsync(movieId);

            if (movie != null)
            {
                var movieCategories = await _movieCategoryRepository.GetWhere(m=>m.MovieId == movieId).ToListAsync();

                List<CategoryResponse> categories = movieCategories.Select(mc => _mapster.Map<CategoryResponse>(mc.Category)).ToList();

                MovieResponse movieResponse = _mapster.Map<MovieResponse>(movie);

                movieResponse.Categories = categories;

                return movieResponse;
            }

            return null;
        }

        public async Task<List<MovieResponse>> GetAllMoviesWithCategories()
        {
            List<Movie> movies = await GetAllAsync();

            List<MovieResponse> moviesResponses = new List<MovieResponse>();

            foreach (var movie in movies)
            {
                var movieCategories = await _movieCategoryRepository.GetWhere(m => m.MovieId == movie.Id).ToListAsync();

                List<CategoryResponse> categories = movieCategories.Select(mc => _mapster.Map<CategoryResponse>(mc.Category)).ToList();

                MovieResponse movieResponse = _mapster.Map<MovieResponse>(movie);

                movieResponse.Categories = categories;

                moviesResponses.Add(movieResponse);
            }

            return moviesResponses;
        }
        
        public async Task RateMovie(Guid movieId, int rating)
        {
            Movie? movie = await FindByIdAsync(movieId);

            if (movie.Ratings == null)
            {
                movie.Ratings = new List<int>();
            }
            
            if (movie != null)
            {
                movie.Ratings.Add(rating);

                await Update(movie);
            }
        }

        // Или же можно сделать что бы возвращалось просто айдишники фильмов, а потом запрос на них
        public async Task<List<MovieResponse>> GetRecommendedMovies(Guid userId)
        {
            var userTickets = await _ticketRepository.GetWhere(t => t.ApplicationUserId == userId).Include(t => t.Session.Movie).ToListAsync();

            var userSessions = userTickets.Select(t => t.Session).ToList();

            var watchedMovies = userSessions.Select(s => s.Movie).ToList();

            var watchedMoviesCategories = await _movieCategoryRepository.GetWhere(mc => watchedMovies.Select(m => m.Id).Contains(mc.MovieId)).ToListAsync();

            var watchedCategories = watchedMoviesCategories.Select(mc => mc.Category).ToList();

            var mostFrequentCategories = watchedCategories
                .GroupBy(c => c)
                .OrderByDescending(g => g.Count())
                .Select(g => g.Key)
                .Take(3)
                .ToList();

            var recommendedMoviesCategories = await _movieCategoryRepository.GetWhere(mc => mostFrequentCategories.Contains(mc.Category)).ToListAsync();

            var recommendedMovies = recommendedMoviesCategories.Select(mc => mc.Movie).Distinct().ToList();

            var recommendedMoviesResponses = recommendedMovies.Select(m => _mapster.Map<MovieResponse>(m)).ToList();

            return recommendedMoviesResponses ?? [];
        }
    }
}