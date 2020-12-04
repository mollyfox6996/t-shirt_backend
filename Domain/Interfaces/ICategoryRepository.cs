using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllCategoriesAsync(bool trackChanges);
        Task<Category> GetCategoryAsync(int id, bool trackChanges);
        Task<Category> FindCategoryAsync(Expression<Func<Category, bool>> expession, bool trackChanges);
        void CreateCategory(Category category);
        void DeleteCategory(Category category);
    }
}
