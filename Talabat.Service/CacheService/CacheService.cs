﻿using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Service.Contract;

namespace Talabat.Service.CacheService
{
    public class CacheService : ICacheService
    {
        private readonly IDatabase database;
        public CacheService(IConnectionMultiplexer redis)
        {
            database= redis.GetDatabase();
        }
        public async Task<string> GetCacheKeyAsync(string key)
        {
            var cacheResponse=await database.StringGetAsync(key);
            if (cacheResponse.IsNullOrEmpty) return null;
            {
               return cacheResponse.ToString();
            }
        }

        public async Task SetCacheKeyAsync(string key, object response, TimeSpan expireTime)
        {
            if (response is null) return ;
            var option = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
           await database.StringSetAsync(key,JsonSerializer.Serialize(response,option) ,expireTime);   
        }
    }
}
