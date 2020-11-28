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
        Task<PagedList<TShirt>> GetTshirtByCurrentUserAsync(string userId, TShirtParameters tshirtParameters);
        Task<IEnumerable<Category>> GetCategoriesAsync();
        Task<IEnumerable<Gender>> GetGendersAsync();
        Task<Gender> GetGenderAsync(string name);
        Task<Category> GetCategoryAsync(string name);
        Task<PagedList<TShirt>> GetByAuthorAsync(string authorName, TShirtParameters tshirtParameters);

    }
}
