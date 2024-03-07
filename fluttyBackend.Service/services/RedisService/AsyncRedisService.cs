using System.Text.Json;
using fluttyBackend.Service.HardCodeStrings;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

namespace fluttyBackend.Service.services.RedisService
{
    public class AsyncRedisService
    {
        private readonly ConnectionMultiplexer _redisConnection;

        public AsyncRedisService(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString(
                ConnectionStringNames.RedisConnectionString
            );

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Redis connection string is missing.");
            }

            _redisConnection = ConnectionMultiplexer.Connect(connectionString);
        }

        public async Task<bool> SetAsync<T>(string key, T value, TimeSpan? expiration = null)
        {
            var db = _redisConnection.GetDatabase();
            var serializedValue = JsonSerializer.Serialize(value);
            if (expiration.HasValue)
            {
                return await db.StringSetAsync(key, serializedValue, expiration);
            }
            else
            {
                return await db.StringSetAsync(key, serializedValue);
            }
        }

        public async Task<T> GetAsync<T>(string key)
        {
            var db = _redisConnection.GetDatabase();
            var serializedValue = await db.StringGetAsync(key);

            return serializedValue.HasValue
                ? JsonSerializer.Deserialize<T>(serializedValue)
                : default;
        }

        public async Task<bool> RemoveAsync(string key)
        {
            var db = _redisConnection.GetDatabase();
            return await db.KeyDeleteAsync(key);
        }
    }
}