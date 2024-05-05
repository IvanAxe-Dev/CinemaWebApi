using Cinema.Core.Domain.Entities;
using Cinema.Core.DTO;
using Cinema.Core.ServiceContracts;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : BaseController
    {
        private readonly IService<Movie?> _movieService;
        private readonly IMapper _mapster;

        public MovieController(IService<Movie?> movieService, IMapper mapster)
        {
            _movieService = movieService;
            _mapster = mapster;
            
        }

        [HttpGet]
        public async Task<ActionResult<List<Movie>>> GetAll()
        {
            List<Movie?> movies = await _movieService.GetAllAsync();
            return Ok(movies);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Movie>> GetById(Guid id)
        {
            Movie? movie = await _movieService.FindByIdAsync( id);
            if (movie==null)
            {
                return NotFound("Movie not found");
            }

            return Ok(movie);
        }

        [HttpPost]
        public async Task<ActionResult<Movie>> Create(MovieDto movieDto)
        {
            Movie newMovie = _mapster.Map<Movie>(movieDto);
            Movie? createdMovie = await _movieService.Insert(newMovie);
            return CreatedAtAction(nameof(GetById), new {id = createdMovie!.Id}, createdMovie);
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
            Movie? updatedMovie = await _movieService.Update(movie);
            return Ok(updatedMovie);
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