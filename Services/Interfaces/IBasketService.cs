using System.Threading.Tasks;
using Services.DTOs.BasketDTOs;

namespace Services.Interfaces
{
    public interface IBasketService
    {
        Task<BasketDTO> GetBasketAsync(string id);
        Task<BasketDTO> UpdateBasketAsync(BasketDTO basket);
        Task DeleteBasketAsync(string id);
    }
}
