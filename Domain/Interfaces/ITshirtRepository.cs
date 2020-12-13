using Domain.Entities;
using Domain.RequestFeatures;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ITShirtRepository
    {
        Task<PagedList<TShirt>> GetTShirtListAsync(TShirtParameters tshirtParameters, bool trackChanges);
        Task<PagedList<TShirt>> GetTshirtsByUserAsync(string userName, TShirtParameters tshirtParameters, bool trackChanges);
        Task<TShirt> GetTShirtByIdAsync(int id, bool trackChanges);
        void CreateTShirt(TShirt shirt);
        void UpdateTShirt(TShirt shirt);
        void DeleteTShirt(TShirt shirt);
    }
}
