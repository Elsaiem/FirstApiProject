using StackExchange.Redis;
using Store.G04.Core.Entities;
using Store.G04.Core.RepositoreContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Store.G04.Repository.Repositories
{
    public class BusketRepository : IBusketRepository
    {
        private readonly IDatabase  _database;


        public BusketRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

       

        public async Task<bool> DeleteBusketAsync(string busketID)
        {
         return await _database.KeyDeleteAsync(busketID);    
        }

        public async Task<CustomerBusket> GetBusketAsync(string busketID)
        {
           var busket=await _database.StringGetAsync(busketID);
            return  busket.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBusket>(busket);
        }

        public async Task<CustomerBusket> UpdateBusketAsync(CustomerBusket customerBusket)
        {
           var CreateOrUpdateBusket=  await  _database.StringSetAsync(customerBusket.Id, JsonSerializer.Serialize(customerBusket), TimeSpan.FromDays(30));

            if (CreateOrUpdateBusket is false) return null;
            return await GetBusketAsync(customerBusket.Id);
        
        }
    }
}
