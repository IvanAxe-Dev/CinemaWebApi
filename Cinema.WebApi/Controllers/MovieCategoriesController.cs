using Cinema.Core.Domain.Entities;
using Cinema.Core.DTO;
using Cinema.Core.ServiceContracts;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieCategoriesController : ControllerBase
    {
        private readonly IMovieCategoryService _movieCategoryService;
        private readonly IMapper _mapster;

        public MovieCategoriesController(IMovieCategoryService movieCategoryService, IMapper mapster)
        {
            _movieCategoryService = movieCategoryService;
            _mapster = mapster;
        }
        
        [HttpGet]
        public async Task<ActionResult<List<MovieCategoryResponse>>> GetAll()
        {
            var movieCategories = await _movieCategoryService.GetAllAsync();
            
            return Ok(_mapster.Map<List<MovieCategoryResponse>>(movieCategories));
        }
        
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<MovieCategoryResponse>> GetById(Guid id)
        {
            var movieCategory = await _movieCategoryService.FindByIdAsync(id);
            
            if (movieCategory == null)
            {
                return NotFound();
            }
            
            return Ok(_mapster.Map<MovieCategoryResponse>(movieCategory));
        }
        
        // [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<MovieCategoryResponse>> Create(MovieCategoryDto movieCategoryDto)
        {
            var movieCategory = await _movieCategoryService.Insert(_mapster.Map<MovieCategory>(movieCategoryDto));
            
            return CreatedAtAction(nameof(GetById), new { id = movieCategory.Id }, _mapster.Map<MovieCategoryResponse>(movieCategory));
        }

        // [Authorize(Roles = "Admin")]
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<MovieCategoryResponse>> Update(Guid id, MovieCategoryDto movieCategoryDto)
        {
            var existingMovieCategory = await _movieCategoryService.FindByIdAsync(id);

            if (existingMovieCategory == null)
            {
                return NotFound();
            }

            var movieCategory = _mapster.Map(movieCategoryDto, existingMovieCategory);
            
            await _movieCategoryService.Update(movieCategory);
            
            return Ok(_mapster.Map<MovieCategoryResponse>(movieCategory));
        }
        
        // [Authorize(Roles = "Admin")]
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var movieCategory = await _movieCategoryService.FindByIdAsync(id);

            if (movieCategory == null)
            {
                return NotFound();
            }

            await _movieCategoryService.DeleteAsync(movieCategory);
            
            return NoContent();
        }
    }
}
