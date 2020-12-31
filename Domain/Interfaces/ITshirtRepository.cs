using Domain.Entities;
using Domain.RequestFeatures;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ITshirtRepository
    {
        Task<PagedList<TShirt>> GetTShirtListAsync(TshirtParameters tshirtParameters, bool trackChanges);
        Task<TShirt> GetTShirtByIdAsync(int id, bool trackChanges);
        void CreateTShirt(TShirt shirt);
        void UpdateTShirt(TShirt shirt);
        void DeleteTShirt(TShirt shirt);
    }
}
