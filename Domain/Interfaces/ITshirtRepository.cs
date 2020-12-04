using Domain.Entities;
using Domain.RequestFeatures;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ITShirtRepository
    {
<<<<<<< HEAD
        Task<PagedList<TShirt>> GetTShirtListAsync(TShirtParameters tshirtParameters, bool trackChanges);
        Task<PagedList<TShirt>> GetTshirtsByUserAsync(string userId, TShirtParameters tshirtParameters, bool trackChanges);
        Task<TShirt> GetTShirtByIdAsync(int id, bool trackChanges);
        void CreateTShirt(TShirt shirt);
        void UpdateTShirt(TShirt shirt);
        void DeleteTShirt(TShirt shirt);
=======
        Task<PagedList<TShirt>> GetTShirtListAsync(TShirtParameters tshirtParameters);
        Task<TShirt> GetTShirtByIdAsync(int id);
        Task CreateTShirtAsync(TShirt shirt);
        Task<PagedList<TShirt>> GetTshirtByCurrentUserAsync(string userId, TShirtParameters tshirtParameters);
        Task<IEnumerable<Category>> GetCategoriesAsync();
        Task<IEnumerable<Gender>> GetGendersAsync();
        Task<Gender> GetGenderAsync(string name);
        Task<Category> GetCategoryAsync(string name);
        Task<PagedList<TShirt>> GetByAuthorAsync(string authorName, TShirtParameters tshirtParameters);

>>>>>>> refs/remotes/origin/dev
    }
}
