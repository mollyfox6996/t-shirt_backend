using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IRepositoryManager
    {
        ITShirtRepository TShirt { get; }
        IBasketRepository Basket { get; }
        ICategoryRepository Category { get; }
        IGenderRepository Gender { get; }

        Task SaveAsync();
    }
}
