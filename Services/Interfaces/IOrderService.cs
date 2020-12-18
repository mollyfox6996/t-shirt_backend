using System.Collections.Generic;
using System.Threading.Tasks;
using Services.DTOs.OrderAggregate;

namespace Services.Interfaces
{
    public interface IOrderService
    {
        Task<OrderToReturnDTO> CreateOrderAsync(OrderDTO orderDTO, string email);
        Task<IEnumerable<OrderToReturnDTO>> GetOrdersForUserAsync(string email);
        Task<OrderToReturnDTO> GetOrderAsync(int id, string email);
        Task<IEnumerable<DeliveryMethodDTO>> GetDeliveryMethods();
    }
}