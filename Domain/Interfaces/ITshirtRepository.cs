using Domain.Entities;
using Domain.RequestFeatures;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ITshirtRepository
    {
        Task<PagedList<TShirt>> GetTShirtListAsync(TShirtParameters tshirtParameters);
        Task<TShirt> GetTShirtByIdAsync(int id);
        Task CreateTShirtAsync(TShirt shirt);
        Task<IEnumerable<TShirt>> GetTshirtByCurrentUserAsync(string userId);
        Task<IEnumerable<Category>> GetCategoriesAsync();
        Task<IEnumerable<Gender>> GetGendersAsync();
        Task<Gender> GetGenderAsync(string name);
        Task<Category> GetCategoryAsync(string name);
        Task<IEnumerable<TShirt>> GetByAuthorAsync(string authorName);

    }
}
