using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class BasketRepository : IBasketRepository
    {
        private readonly AppDbContext _context;
        
        public BasketRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<CustomBasket> CreateBasketAsync(string id)
        {
            var basket = new CustomBasket(id);
            await _context.CustomBaskets.AddAsync(basket);
            await _context.SaveChangesAsync();
            return basket;
        }
        public async Task DeleteBasketAsync(string id)
        {
            var basket = await _context.CustomBaskets.FindAsync(id);
            _context.CustomBaskets.Remove(basket);
            await _context.SaveChangesAsync();
        }

        public async Task<CustomBasket> GetBasketAsync(string id)
        {
            var basket = await _context.CustomBaskets.Include(p => p.Items).FirstOrDefaultAsync(x => x.Id == id);
            return basket;
        }

        public async Task<CustomBasket> UpdateBasketAsync(CustomBasket basket)
        {
            //await _context.Items.AddRangeAsync(basket.Items);
            _context.CustomBaskets.Update(basket);
            await _context.SaveChangesAsync();
            return basket;
        }
    }
}
