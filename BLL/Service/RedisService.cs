using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    public class RedisService : IRedisService
    {
        private ConnectionMultiplexer connection;
        private IDatabase redis;
        public RedisService(IConfiguration config)
        {
            var connectionString = config.GetValue<string>("Redis:ConnectionString");
            connection = ConnectionMultiplexer.Connect(connectionString);

            redis = connection.GetDatabase();
        }

        public async Task<T> GetAsync<T>(string key)
        {
            if (redis.KeyExists(key))
            {
                var data = await redis.StringGetAsync(key);

                T result = JsonConvert.DeserializeObject<T>(data);

                return result;
            } else
            {
                return default(T);
            }
        }

        public async Task SetAsync(string key, object data)
        {
            await redis.StringSetAsync(key, JsonConvert.SerializeObject(data), TimeSpan.FromSeconds(30));
        }

        public async Task<bool> DeleteAsync(string key)
        {
            return await redis.KeyDeleteAsync(key);
        }
    }
}
