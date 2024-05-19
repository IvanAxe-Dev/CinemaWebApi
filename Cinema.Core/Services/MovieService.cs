using Cinema.Core.Domain.Entities;
using Cinema.Core.Domain.IdentityEntities;
using Cinema.Core.Domain.RepositoryContracts;
using Cinema.Core.DTO;
using Cinema.Core.ServiceContracts;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Cinema.Core.Services
{
    public class MovieService : Service<Movie>, IMovieService
    {
        private readonly IMapper _mapster;
        private readonly IMovieRepository _movieRepository;
        private readonly IMovieCategoryRepository _movieCategoryRepository;
        private readonly ITicketRepository _ticketRepository;
        private readonly IUserMovieRateService _userMovieRateService;
        private readonly UserManager<ApplicationUser> _userManager;

        private const int RECENTLY_WATCHED_CATEGORIES_MAX = 5;

        public MovieService(IMovieRepository repository, IMapper mapster, IMovieCategoryRepository movieCategoryRepository,
            ITicketRepository ticketRepository, UserManager<ApplicationUser> userManager, IUserMovieRateService userMovieRateService) : base(repository)
        {
            _mapster = mapster;
            _movieRepository = repository;
            _movieCategoryRepository = movieCategoryRepository;
            _ticketRepository = ticketRepository;
            _userManager = userManager;
            _userMovieRateService = userMovieRateService;
        }

        public async Task<MovieResponse?> GetMovieWithCategoriesById(Guid movieId, ClaimsPrincipal? user)
        {
            Movie? movie = await FindByIdAsync(movieId);

            if (movie != null)
            {
                var movieCategories = await _movieCategoryRepository.GetWhere(m => m.MovieId == movieId).ToListAsync();

                List<CategoryResponse> categories = movieCategories.Select(mc => _mapster.Map<CategoryResponse>(mc.Category)).ToList();

                MovieResponse movieResponse = _mapster.Map<MovieResponse>(movie);

                movieResponse.Categories = categories;
                if (user != null)
                {
                    var appUser = await _userManager.GetUserAsync(user);
                    movieResponse.UserRating = await _userMovieRateService.GetUserMovieRating(appUser!.Id);
                }
                return movieResponse;
            }

            return null;
        }

        public async Task UpdateRecentlyWatchedCategoriesAsync(ClaimsPrincipal user, IEnumerable<CategoryResponse> categories)
        {
            var appUser = await _userManager.GetUserAsync(user);

            if (appUser == null) return;

            List<string> recentlyWatchedCategories = JsonConvert.DeserializeObject<List<string>>(appUser.RecentlyWatchedCategories);

            recentlyWatchedCategories ??= new List<string>();

            foreach (var category in categories)
            {
                recentlyWatchedCategories = recentlyWatchedCategories.Prepend(category.Name).ToList();
            }

            if (recentlyWatchedCategories.Count > RECENTLY_WATCHED_CATEGORIES_MAX)
            {
                for (int i = RECENTLY_WATCHED_CATEGORIES_MAX; i < recentlyWatchedCategories.Count; i++)
                {
                    appUser.RecentlyWatchedCategories.Remove(appUser.RecentlyWatchedCategories.ElementAt(i));
                }
            }

            appUser.RecentlyWatchedCategories = JsonConvert.SerializeObject(recentlyWatchedCategories);

            await _userManager.UpdateAsync(appUser);
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

        public async Task RateMovie(Guid movieId, ClaimsPrincipal user, int rating)
        {
            Movie? movie = await FindByIdAsync(movieId);

            movie!.Ratings ??= new List<int>();
            var appUser = await _userManager.GetUserAsync(user);


            movie.Ratings.Add(rating);

            UserMovieRate userMovieRate = new()
            {
                Movie = movie,
                ApplicationUserId = appUser!.Id,
                Rating = rating
            };

            await Update(movie);

            await _userMovieRateService.Insert(userMovieRate);
        }

        public async Task<List<MovieResponse>> GetRecommendedMovies(ClaimsPrincipal user)
        {
            var appUser = await _userManager.GetUserAsync(user);

            List<string>? recentlyWatchedCategories = JsonConvert.DeserializeObject<List<string>>(appUser.RecentlyWatchedCategories);

            recentlyWatchedCategories ??= new List<string>();

            List<Movie> recommendedMovies = await _movieCategoryRepository.GetWhere(mc => recentlyWatchedCategories.Contains(mc.Category.Name))
                .Select(mc => mc.Movie)
                .ToListAsync();

            List<MovieResponse> movieResponses = new List<MovieResponse>();

            foreach (var movie in recommendedMovies)
            {
                movieResponses.Add(await GetMovieWithCategoriesById(movie.Id, user));
            }

            return movieResponses;
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
                movies = movies.Where(m => m.Title.Contains(request.SearchTerm, StringComparison.OrdinalIgnoreCase)).ToList();
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

        public async Task UploadImageToMovie(Guid id, string imageUrl)
        {
            Movie? movie = await FindByIdAsync(id);

            if (movie != null)
            {
                movie.ImageUrl = imageUrl;

                await Update(movie);
            }
        }
    }
}