namespace Basket.Host.Services.Interfaces
{
    public interface ICacheService
    {
        public Task<bool> AddOrUpdateAsync<T>(string key, T value);
        public Task<T?> GetAsync<T>(string key);
        public Task<bool> Remove(string key);
    }
}
