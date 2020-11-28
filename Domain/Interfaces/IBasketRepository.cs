using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IBasketRepository
    {
        Task<CustomBasket> CreateBasketAsync(string id);
        Task<CustomBasket> GetBasketAsync(string id);
        Task<CustomBasket> UpdateBasketAsync(CustomBasket basket);
        Task DeleteBasketAsync(string id);
    }
}
