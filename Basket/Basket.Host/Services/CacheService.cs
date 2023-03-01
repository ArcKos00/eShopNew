using Basket.Host.Configurations;
using Basket.Host.Services.Interfaces;
using Infrastructure.Services.Interfaces;

namespace Basket.Host.Services
{
    public class CacheService : ICacheService
    {
        private readonly ILogger<CacheService> _logger;
        private readonly IRedisCacheConnectionService _redisService;
        private readonly IJsonSerializer _serializer;
        private readonly RedisConfig _config;

        public CacheService(
            ILogger<CacheService> logger,
            IRedisCacheConnectionService redisService,
            IJsonSerializer serializer,
            IOptions<RedisConfig> config)
        {
            _logger = logger;
            _redisService = redisService;
            _serializer = serializer;
            _config = config.Value;
        }

        public Task<bool> AddOrUpdateAsync<T>(string key, T value) => AddOrUpdateInternalAsync(key, value);

        public async Task<T?> GetAsync<T>(string key)
        {
            var redis = GetRedisDatabase();
            var cacheKey = GetItemCacheKey(key);
            var serialized = await redis.StringGetAsync(cacheKey);

            return serialized.HasValue ? _serializer.Deserialize<T>(serialized.ToString()) : default(T) !;
        }

        public async Task<bool> Remove(string key)
        {
            var redis = GetRedisDatabase();
            var cacheKey = GetItemCacheKey(key);
            return await redis.KeyDeleteAsync(cacheKey);
        }

        private async Task<bool> AddOrUpdateInternalAsync<T>(string key, T value, IDatabase redis = null!, TimeSpan? expiry = null)
        {
            redis = redis ?? GetRedisDatabase();
            expiry = expiry ?? _config.CacheTimeout;

            var cacheKey = GetItemCacheKey(key);
            var serialized = _serializer.Serialize(value);
            var result = await redis.StringSetAsync(cacheKey, serialized, expiry);
            if (result)
            {
                _logger.LogInformation($"{LoggerDefaultResponse.ValueCached}{cacheKey}");
            }
            else
            {
                _logger.LogInformation($"{LoggerDefaultResponse.ValueUpdated}{cacheKey}");
            }

            return result;
        }

        private string GetItemCacheKey(string key) => $"{key.GetHashCode()}{key}";

        private IDatabase GetRedisDatabase() => _redisService.Connection.GetDatabase();
    }
}
