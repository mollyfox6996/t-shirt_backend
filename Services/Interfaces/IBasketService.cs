using Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IBasketService
    {
<<<<<<< HEAD
        Task<BasketDTO> GetBasketAsync(string id, bool trackChanges);
        Task<BasketDTO> UpdateBasketAsync(BasketDTO basket);
        Task DeleteBasketAsync(BasketDTO basket);
=======
        Task<BasketDTO> GetBasketAsync(string id);
        Task<BasketDTO> UpdateBasketAsync(BasketDTO basket);
        Task DeleteBasketAsync(string id);

>>>>>>> refs/remotes/origin/dev
    }
}
