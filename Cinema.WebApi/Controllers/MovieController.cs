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
        public async Task<ActionResult<List<Movie>>> GetAll()
        {
            List<Movie> movies = await _movieService.GetAllAsync();

            return Ok(movies);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Movie>> GetById(Guid id)
        {
            Movie? movie = await _movieService.FindByIdAsync(id);

            if (movie == null)
            {
                return NotFound("Movie not found");
            }

            return Ok(movie);
        }

        [HttpPost]
        public async Task<ActionResult<Movie>> Create(MovieDto movieDto)
        {
            Movie movie = _mapster.Map<Movie>(movieDto);
            
            Movie newMovie = await _movieService.Insert(movie);


            return CreatedAtAction(nameof(GetById), new { id = newMovie.Id }, newMovie);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Movie>> Update(Guid id, MovieDto movieDto)
        {
            Movie? existingMovie = await _movieService.FindByIdAsync(id);

            if (existingMovie == null)
            {
                return NotFound("Movie not found");
            }

            Movie movie = _mapster.Map(movieDto, existingMovie);

            return Ok(await _movieService.Update(movie));
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