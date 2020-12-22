using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Services.Interfaces;

namespace Services.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepositoryManager _repositoryManager;
        
        public CategoryService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task CreateAsync(Category category)
        {
            _repositoryManager.Category.CreateCategory(category);
            await _repositoryManager.SaveAsync();
        }

        public async Task DeleteAsync(Category category)
        {
            _repositoryManager.Category.DeleteCategory(category);
            await _repositoryManager.SaveAsync();
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync(bool trackChanges) =>
            await _repositoryManager.Category.GetAllCategoriesAsync(trackChanges);

        public async Task<Category> GetByIdAsync(int id, bool trackChanges) => 
            await _repositoryManager.Category.FindCategoryAsync(c => c.Id == id, false);

        public async Task<Category> FindCategoryAsync(Expression<Func<Category, bool>> expression, bool trackChanges) =>
            await _repositoryManager.Category.FindCategoryAsync(expression, trackChanges);
    }
}