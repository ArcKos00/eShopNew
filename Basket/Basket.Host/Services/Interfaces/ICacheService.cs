namespace Basket.Host.Services.Interfaces
{
    public interface ICacheService
    {
        public Task AddOrUpdateAsync<T>(string key, T value);
        public Task<T?> GetAsync<T>(string key);
        public Task Remove(string key);
    }
}
