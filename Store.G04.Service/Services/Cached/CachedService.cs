using StackExchange.Redis;
using Store.G04.Core.ServicesContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Store.G04.Service.Services.Cached
{
    public class CachedService : ICacheService
    {
        private readonly IDatabase _database;
        public CachedService(IConnectionMultiplexer redis)
        {
          _database= redis.GetDatabase();


        }

        public async Task<string> GetCacheKeyAsync(string Key)
        {
            var cacheResponse = await _database.StringGetAsync(Key);
            if (cacheResponse.IsNullOrEmpty) return null;
            return cacheResponse.ToString();
        }

        public async Task SetCacheKeyAsync(string Key, object Response, TimeSpan expireTime)
        {
            if (Response is null) return;
            var options = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };

            await _database.StringSetAsync(Key, JsonSerializer.Serialize(Response, options), expireTime);
        }
    }
}
