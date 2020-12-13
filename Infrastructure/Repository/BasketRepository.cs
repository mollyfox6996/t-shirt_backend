using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class BasketRepository : RepositoryBase<CustomBasket>, IBasketRepository
    {
        public BasketRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<CustomBasket> GetBasketAsync(string id, bool trackChanges) => 
            await FindByCondition(c => c.Id.Equals(id), trackChanges)
                .SingleOrDefaultAsync();

        public CustomBasket CreateBasket(string id)
        {
            var basket = new CustomBasket(id);
            Create(basket);
            
            return basket;
        }

        public void UpdateBasket(CustomBasket basket) => Update(basket);

        public void DeleteBasket(CustomBasket basket) => Delete(basket);
    }
}
