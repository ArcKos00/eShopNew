#pragma warning disable CS8618
namespace Basket.Host.Configurations
{
    public class RedisConfig
    {
        public string Host { get; set; }
        public TimeSpan CacheTimeout { get; set; }
    }
}
