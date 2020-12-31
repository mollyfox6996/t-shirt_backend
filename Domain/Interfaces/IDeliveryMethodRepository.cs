using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities.OrderAggregate;

namespace Domain.Interfaces
{
    public interface IDeliveryMethodRepository
    {
        
        Task<IEnumerable<DeliveryMethod>> GetDeliveryMethodsAsync(bool trackChanges);
        Task<DeliveryMethod> GetDeliveryMethodAsync(int id);
        void CreateMethod(DeliveryMethod method);
        void DeleteMethod(DeliveryMethod method);
    }
}