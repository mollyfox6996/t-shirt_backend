using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities.OrderAggregate;

namespace Domain.Interfaces
{
    public interface IOrderRepository
    {
        void CreateOrder(Order order);
        Task<IEnumerable<Order>> GetOrdersForUserAsync(string email);
        Task<Order> GetOrderByIdAsync(int id, string email);

        Task<IEnumerable<Order>> GetOrdersAsync();
        void DeleteOrder(Order order);
       
    }
}