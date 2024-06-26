using Cinema.Core.Domain.Entities;
using Cinema.Core.DTO;
using Cinema.Core.ServiceContracts;
using Cinema.Core.Services;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : BaseController
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapster;

        public CategoryController(ICategoryService categoryService, IMapper mapster)
        {
            _categoryService = categoryService;
            _mapster = mapster;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<List<Category>>> GetAll()
        {
            List<Category> categories = await _categoryService.GetAllAsync();

            return Ok(categories);
        }

        [AllowAnonymous]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Category>> GetById(Guid id)
        {
            Category? category = await _categoryService.FindByIdAsync(id);

            if (category == null)
            {
                return NotFound("Category not found");
            }
            return Ok(category);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<Category>> Create(CategoryDto categoryDto)
        {
            Category newCategory = await _categoryService.Insert(_mapster.Map<Category>(categoryDto));

            return CreatedAtAction(nameof(GetById), new { id = newCategory.Id }, newCategory);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Category>> Update(Guid id, CategoryDto categoryDto)
        {
            Category? existingCategory = await _categoryService.FindByIdAsync(id);

            if (existingCategory == null)
            {
                return NotFound("Category not found");
            }

            Category category = _mapster.Map(categoryDto, existingCategory);
            Category updatedCategory = await _categoryService.Update(category);

            return Ok(updatedCategory);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            Category? existingCategory = await _categoryService.FindByIdAsync(id);

            if (existingCategory == null)
            {
                return NotFound("Category not found");
            }

            await _categoryService.DeleteAsync(existingCategory);
            return NoContent();
        }
    }
}
