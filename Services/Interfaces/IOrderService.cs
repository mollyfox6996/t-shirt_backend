using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities.OrderAggregate;
using Services.DTOs.OrderAggregate;

namespace Services.Interfaces
{
    public interface IOrderService
    {
        Task<OrderToReturnDTO> CreateOrderAsync(OrderDTO orderDTO, string email);
        Task<IEnumerable<OrderToReturnDTO>> GetOrdersForUserAsync(string email);
        Task<OrderToReturnDTO> GetOrderAsync(int id, string email);
        Task CreateDeliveryMethodAsync(DeliveryMethod method);
        Task DeleteDeliveryMethodAsync(int id);
        Task<IEnumerable<DeliveryMethodDTO>> GetDeliveryMethods();
        Task<IEnumerable<OrderToReturnDTO>> GetAllOrdersAsync();
    }
}