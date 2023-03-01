using Basket.Host.Configurations;
using Basket.Host.Services.Interfaces;

namespace Basket.Host.Services
{
    public class RedisCacheConnectionService : IRedisCacheConnectionService, IDisposable
    {
        private readonly Lazy<ConnectionMultiplexer> _connectionLazy;
        private bool _disposed;

        public RedisCacheConnectionService(IOptions<RedisConfig> options)
        {
            var config = ConfigurationOptions.Parse(options.Value.Host);
            _connectionLazy = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(config));
        }

        public IConnectionMultiplexer Connection => _connectionLazy.Value;

        public void Dispose()
        {
            if (!_disposed)
            {
                Connection.Dispose();
                _disposed = true;
            }
        }
    }
}