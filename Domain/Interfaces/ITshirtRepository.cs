using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ITshirtRepository
    {
        Task<IEnumerable<TShirt>> GetTShirtListAsync();
        Task<TShirt> GetTShirtByIdAsync(int id);
        Task CreateTShirtAsync(TShirt shirt);
        Task<IEnumerable<TShirt>> GetTshirtByCurrentUserAsync(string userId);
        Task<IEnumerable<Category>> GetCategoriesAsync();
        Task<Category> GetCategoryAsync(string name);
        Task<IEnumerable<TShirt>> GetByAuthorAsync(string authorName);

    }
}
