using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IBasketRepository
    {
        Task<CustomBasket> GetBasketAsync(string id, bool trackChanges);
        CustomBasket CreateBasket(string id);
        void UpdateBasket(CustomBasket basket);
        void DeleteBasket(CustomBasket customBasket);
    }
}
