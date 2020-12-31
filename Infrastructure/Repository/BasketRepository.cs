using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;
        public BasketRepository(IConnectionMultiplexer database)
        {
            _database = database.GetDatabase();
        }

        public async Task<CustomBasket> GetBasketAsync(string id)
        {
            var data = await _database.StringGetAsync(id);
            return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomBasket>(data);
        }
        public async Task<CustomBasket> UpdateBasketAsync(CustomBasket basket)
        {
            
            var created = await _database.StringSetAsync(basket.Id, JsonSerializer.Serialize(basket), TimeSpan.FromDays(15));

            if (!created) return null;

            return await GetBasketAsync(basket.Id);
        }
        public async Task<bool> DeleteBasketAsync(string id)
        {
            return await _database.KeyDeleteAsync(id);
        }
    }
}
