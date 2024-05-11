using Cinema.Core.Domain.Entities;
using Cinema.Core.DTO;
using Cinema.Core.ServiceContracts;
using Cinema.Core.Services;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : BaseController
    {
        private readonly IMovieService _movieService;
        private readonly IMapper _mapster;

        public MovieController(IMovieService movieService, IMapper mapster)
        {
            _movieService = movieService;
            _mapster = mapster;
        }

        [HttpGet]
        public async Task<ActionResult<List<MovieResponse>>> GetAll()
        {
            List<MovieResponse> movies = await _movieService.GetAllMoviesWithCategories();

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

            return Ok(movie);
        }

        [HttpPost]
        public async Task<ActionResult<MovieResponse>> Create(MovieDto movieDto)
        {
            Movie movie = _mapster.Map<Movie>(movieDto);
            
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
            List<MovieResponse> movies = await _movieService.GetRecommendedMovies(userId);

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
    }
}