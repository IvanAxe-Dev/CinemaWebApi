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

        public MovieService(IMovieRepository repository, IMapper mapster, IMovieCategoryRepository movieCategoryRepository) : base(repository)
        {
            _mapster = mapster;
            _movieRepository = repository;
            _movieCategoryRepository = movieCategoryRepository;
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

        public async Task<List<MovieResponse>> TakeNLatestMovies(int? moviesToTake)
        {
            var movies = await GetAllMoviesWithCategories();

            var latestMovies = movies.OrderByDescending(movie => movie.RentalStartDate).ToList();

            //var movies = await GetAllAsync();

            //var latestMovies = movies
            //    .Select(m =>
            //        m.Sessions
            //            .OrderByDescending(s => s.Date.Date)
            //            .ThenByDescending(s => s.StartTime.Hour)
            //            .ThenByDescending(s => s.StartTime.Minute));

            if (moviesToTake is not null)
            {
                latestMovies = latestMovies.Take((int)moviesToTake).ToList();
            }

            return _mapster.Map<List<MovieResponse>>(latestMovies);
        }

        public async Task<List<MovieResponse>> GetFilteredMovies(GetMoviesQuery request)
        {
            var movies = await GetAllMoviesWithCategories();

            if (request.Categories is not null)
            {
                string[] categories = request.Categories.Split(',').Select(c => c.ToLower()).ToArray();
                movies = movies
                    .Where(m => m.Categories
                        .Any(c => categories
                            .Contains(c.Name)))
                    .ToList();
            }

            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                movies = movies.Where(m => m.Title.Contains(request.SearchTerm)).ToList();
            }

            if (request.DateStartInterval is not null && request.DateEndInterval is not null)
            {
                movies = movies.Where(m =>
                    m.Sessions.Any(s => s.Date >= DateOnly.FromDateTime((DateTime)request.DateStartInterval) && s.Date <= DateOnly.FromDateTime((DateTime)request.DateEndInterval))).ToList();
            }

            if (request.TimeStartInterval is not null && request.TimeEndInterval is not null)
            {
                movies = movies.Where(m =>
                    m.Sessions.Any(s => s.StartTime >= TimeOnly.FromDateTime((DateTime)request.TimeStartInterval) && s.StartTime <= TimeOnly.FromDateTime((DateTime)request.TimeEndInterval))).ToList();
            }

            return movies;
        }
    }
}