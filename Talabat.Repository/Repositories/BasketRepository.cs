using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;

namespace Talabat.Repository.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase database;
        public BasketRepository(IConnectionMultiplexer redis) 
        {
           database= redis.GetDatabase();
        }
        public async Task<bool> DeleteBasketAsync(string id)
        {
           return await database.KeyDeleteAsync(id);    
        }

        public async Task<CustomerBasket?> GetBasketAsync(string id)
        {
            var basket=await database.StringGetAsync(id);
            return basket.IsNullOrEmpty ? null:JsonSerializer.Deserialize<CustomerBasket>(basket);
        }

        public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket)
        {
           var createOrUpdate= await database.StringSetAsync(basket.Id,JsonSerializer.Serialize(basket),TimeSpan.FromDays(15));
            if (createOrUpdate is false)
            {
                return null;
            }
            return await GetBasketAsync(basket.Id);
        }
    }
}
