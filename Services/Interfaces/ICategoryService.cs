using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain.Entities;

namespace Services.Interfaces
{
    public interface ICategoryService
    {
        Task CreateAsync(Category category);
        Task DeleteAsync(Category category);
        Task<IEnumerable<Category>> GetAllCategoriesAsync(bool trackChanges);
        Task<Category> GetByIdAsync(int id, bool trackChanges);
        Task<Category> FindCategoryAsync(Expression<Func<Category, bool>> expression, bool trackChanges);
    }
}