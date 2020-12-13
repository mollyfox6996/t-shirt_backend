using Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IBasketService
    {
        Task<BasketDTO> GetBasketAsync(string id, bool trackChanges);
        Task<BasketDTO> UpdateBasketAsync(BasketDTO basket);
        Task DeleteBasketAsync(BasketDTO basket);
    }
}
