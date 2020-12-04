using Domain.Entities;
<<<<<<< HEAD
using System.Collections.Generic;
=======
using System;
using System.Collections.Generic;
using System.Text;
>>>>>>> refs/remotes/origin/dev
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IBasketRepository
    {
<<<<<<< HEAD
        Task<CustomBasket> GetBasketAsync(string id, bool trackChanges);
        CustomBasket CreateBasket(string id);
        void UpdateBasket(CustomBasket basket);
        void DeleteBasket(CustomBasket customBasket);
=======
        Task<CustomBasket> CreateBasketAsync(string id);
        Task<CustomBasket> GetBasketAsync(string id);
        Task<CustomBasket> UpdateBasketAsync(CustomBasket basket);
        Task DeleteBasketAsync(string id);
>>>>>>> refs/remotes/origin/dev
    }
}
