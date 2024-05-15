using Cinema.Core.Domain.Entities;
using Cinema.Core.Domain.IdentityEntities;
using Cinema.Core.DTO;
using Cinema.Core.ServiceContracts;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : BaseController
    {
        private readonly IMovieService _movieService;
        private readonly IMapper _mapster;
        private readonly UserManager<ApplicationUser> _userManager;

        public MovieController(IMovieService movieService, IMapper mapster,
            UserManager<ApplicationUser> userManager)
        {
            _movieService = movieService;
            _mapster = mapster;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult<List<MovieResponse>>> GetAll([FromQuery]GetMoviesQuery moviesQuery)
        {
            //List<MovieResponse> movies = await _movieService.GetAllMoviesWithCategories();

            var movies = await _movieService.GetFilteredMovies(moviesQuery);
            return Ok(movies);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<MovieResponse>> GetById(Guid id)
        {
            MovieResponse? movie = await _movieService.GetMovieWithCategoriesById(id);

            if (movie == null)
            {
                return NotFound("Movie not found");
            }
            
            if (User?.Identity?.IsAuthenticated == true)
            {
                ApplicationUser? user = await _userManager.GetUserAsync(User);

                if (user != null)
                {
                    List<Category> categories = movie.Categories.Select(c => _mapster.Map<Category>(c)).ToList();

                    List<string> categoriesNames = categories.Select(c => c.Name).ToList();
            
                    if (user.RecentlyWatchedCategories == null)
                    {
                        user.RecentlyWatchedCategories = new List<string>();
                    }

                    user.RecentlyWatchedCategories.AddRange(categoriesNames);
            
                    if (user.RecentlyWatchedCategories.Count > 10)
                    {
                        user.RecentlyWatchedCategories.RemoveRange(0, user.RecentlyWatchedCategories.Count - 10);
                    }
            
                    await _userManager.UpdateAsync(user);
                }
            }

            return Ok(movie);
        }

        [HttpPost]
        public async Task<ActionResult<MovieResponse>> Create(MovieDto movieDto)
        {
            Movie movie = _mapster.Map<Movie>(movieDto);

            movie.Id = Guid.NewGuid();

            Movie newMovie = await _movieService.Insert(movie);

            return CreatedAtAction(nameof(GetById), new { id = newMovie.Id }, _mapster.Map<MovieResponse>(newMovie));
        }
        
        [HttpPost("{id:guid}/rate")]
        public async Task<ActionResult<MovieResponse>> Rate(Guid id, [FromBody] int rating)
        {
            if (rating < 1 || rating > 10)
            {
                return BadRequest("Rating must be between 1 and 5");
            }
            
            Movie? movie = await _movieService.FindByIdAsync(id);
            
            if (movie == null)
            {
                return NotFound("Movie not found");
            }
            
            await _movieService.RateMovie(id, rating);
            
            return Ok("Movie rated successfully");
        }
        
        [HttpGet("{userId:guid}/recommended")]
        public async Task<ActionResult<List<MovieResponse>>> GetRecommendedMovies(Guid userId)
        {
            ApplicationUser? user = await _userManager.FindByIdAsync(userId.ToString());
            
            if (user == null)
            {
                return Unauthorized("User not found");
            }
            
            List<MovieResponse> movies = await _movieService.GetRecommendedMovies(user);

            return Ok(movies);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<MovieResponse>> Update(Guid id, MovieDto movieDto)
        {
            Movie? existingMovie = await _movieService.FindByIdAsync(id);

            if (existingMovie == null)
            {
                return NotFound("Movie not found");
            }

            Movie movie = _mapster.Map(movieDto, existingMovie);
            
            return Ok(_mapster.Map<MovieResponse>(await _movieService.Update(movie)));
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            Movie? movie = await _movieService.FindByIdAsync(id);

            if (movie == null)
            {
                return NotFound("Movie not found");
            }

            await _movieService.DeleteAsync(movie);
            return NoContent();
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<List<MovieResponse>>> GetLatestMovies([FromQuery] int? moviesToTake)
        {
            List<MovieResponse> movies = await _movieService.TakeNLatestMovies(moviesToTake);

            return Ok(movies);
        }
    }
}