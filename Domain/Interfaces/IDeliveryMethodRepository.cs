using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities.OrderAggregate;

namespace Domain.Interfaces
{
    public interface IDeliveryMethodRepository
    {
        
        Task<IEnumerable<DeliveryMethod>> GetDeliveryMethodsAsync();
    }
}