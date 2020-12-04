using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync(bool trackChanges) =>
            await FindAll(trackChanges)
                .ToListAsync();
       
        public async Task<Category> GetCategoryAsync(int id, bool trackChanges) =>
            await FindByCondition(c => c.Id == id, trackChanges)
                .SingleOrDefaultAsync();
        
        public async Task<Category> FindCategoryAsync(Expression<Func<Category, bool>> expression, bool trackChanges) =>
            await FindByCondition(expression, trackChanges)
                .SingleOrDefaultAsync();

        public void CreateCategory(Category category) => Create(category);

        public void UpdateCategory(Category category) => Update(category);

        public void DeleteCategory(Category category) => Delete(category);
    }
}
