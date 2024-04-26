using Cinema.Core.Domain.RepositoryContracts;
using Cinema.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Core.Services
{
    public class CategoryService
    {
        private readonly IRepository<Category> _categoryRepository;

        public CategoryService(IRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            return await _categoryRepository.GetAll().ToListAsync();
        }

        public async Task<Category?> GetCategoryByIdAsync(Guid id)
        {
            return await _categoryRepository.GetFirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddCategoryAsync(Category category)
        {
            await _categoryRepository.Post(category);
            //await _categoryRepository.SaveChangesAsync();
        }

        public void UpdateCategory(Category category)
        {
            _categoryRepository.Update(category);
        }

        public void DeleteCategory(Category category)
        {
            _categoryRepository.Delete(category);
        }
    }
}
