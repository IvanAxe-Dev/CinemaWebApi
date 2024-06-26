using Cinema.Core.Domain.Entities;
using Cinema.Core.Domain.IdentityEntities;
using Cinema.Core.DTO;
using Cinema.Core.ServiceContracts;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
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

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<List<MovieResponse>>> GetAll([FromQuery] GetMoviesQuery moviesQuery)
        {
            //List<MovieResponse> movies = await _movieService.GetAllMoviesWithCategories();

            var movies = await _movieService.GetFilteredMovies(moviesQuery);
            return Ok(movies);
        }

        [AllowAnonymous]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<MovieResponse>> GetById(Guid id)
        {
            MovieResponse? movie = User.Identity.IsAuthenticated
                ? await _movieService.GetMovieWithCategoriesById(id, User)
                : await _movieService.GetMovieWithCategoriesById(id, null);
            
            if (movie == null)
            {
                return NotFound("Movie not found");
            }

            if (User.Identity?.IsAuthenticated == true)
            {
                await _movieService.UpdateRecentlyWatchedCategoriesAsync(User, movie.Categories);
            }

            return Ok(movie);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<MovieResponse>> Create(MovieDto movieDto)
        {
            Movie movie = _mapster.Map<Movie>(movieDto);

            Movie newMovie = await _movieService.Insert(movie);

            return CreatedAtAction(nameof(GetById), new { id = newMovie.Id }, _mapster.Map<MovieResponse>(newMovie));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("{id:guid}/upload-image")]
        public async Task<ActionResult> UploadImageToMovie(Guid id, IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                if (file.Length > 200 * 1024)
                {
                    return BadRequest("File size must be less than 200KB");
                }

                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    byte[] image = memoryStream.ToArray();
                    var base64 = Convert.ToBase64String(image);

                    await _movieService.UploadImageToMovie(id, base64);
                }

                return Ok("Image uploaded successfully");
            }
            else
            {
                return BadRequest("No file selected");
            }
        }

        [Authorize(Roles = "User")]
        [HttpPost("{id:guid}/rate")]
        public async Task<ActionResult<MovieResponse>> Rate(Guid id, [FromBody] int rating)
        {
            if (rating < 1 || rating > 5)
            {
                return BadRequest("Rating must be between 1 and 5");
            }

            Movie? movie = await _movieService.FindByIdAsync(id);

            if (movie == null)
            {
                return NotFound("Movie not found");
            }

            await _movieService.RateMovie(id, User, rating);

            return Ok("Movie rated successfully");
        }

        [Authorize(Roles = "User")]
        [HttpGet("recommended")]
        public async Task<ActionResult<List<MovieResponse>>> GetRecommendedMovies()
        {
            List<MovieResponse> movies = await _movieService.GetRecommendedMovies(User);

            return Ok(movies);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<MovieResponse>> Update(Guid id, MovieUpdateRequest movieDto)
        {
            Movie? existingMovie = await _movieService.FindByIdAsync(id);

            if (existingMovie == null)
            {
                return NotFound("Movie not found");
            }

            Movie movie = _mapster.Map(movieDto, existingMovie);

            return Ok(_mapster.Map<MovieResponse>(await _movieService.Update(movie)));
        }

        [Authorize(Roles = "Admin")]
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

        [AllowAnonymous]
        [HttpGet("[action]")]
        public async Task<ActionResult<List<MovieResponse>>> GetLatestMovies([FromQuery] int? moviesToTake)
        {
            List<MovieResponse> movies = await _movieService.TakeNLatestMovies(moviesToTake);

            return Ok(movies);
        }
    }
}