using Cinema.Core.Domain.Entities;
using Cinema.Core.DTO;
using Cinema.Core.ServiceContracts;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;

namespace Cinema.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : BaseController
    {
        private readonly IService<Category> _categoryService;
        private readonly IMapper _mapster;

        public CategoryController(IService<Category> categoryService, IMapper mapster)
        {
            _categoryService = categoryService;
            _mapster = mapster;
        }
        
        [HttpGet]
        public async Task<ActionResult<List<Category>>> GetAll()
        {
            var categories = await _categoryService.GetAllAsync();
            return Ok(categories);
        }
        
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Category>> GetById(Guid id)
        {
            var category = await _categoryService.FindByIdAsync(id);
            if (category == null)
            {
                return NotFound("Category not found");
            }
            return Ok(category);
        }
        
        [HttpPost]
        public async Task<ActionResult<Category>> Create(CategoryDto categoryDto)
        {
            var newCaregory = _mapster.Map<Category>(categoryDto);
            var createdCategory = await _categoryService.Insert(newCaregory);
            return CreatedAtAction(nameof(GetById), new { id = createdCategory.Id }, createdCategory);
        }
        
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Category>> Update(Guid id, CategoryDto categoryDto)
        {
            var existingCategory = await _categoryService.FindByIdAsync(id);
            if (existingCategory == null)
            {
                return NotFound("Category not found");
            }
            var category = _mapster.Map(categoryDto, existingCategory);
            var updatedCategory = await _categoryService.Update(category);
            return Ok(updatedCategory);
        }
        
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var existingCategory = await _categoryService.FindByIdAsync(id);
            if (existingCategory == null)
            {
                return NotFound("Category not found");
            }
            await _categoryService.DeleteAsync(existingCategory);
            return NoContent();
        }
    }
}
