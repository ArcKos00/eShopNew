using Basket.Host.Configurations;
using Basket.Host.Models.Basket;
using Basket.Host.Services.Interfaces;
using Infrastructure.Services.Interfaces;

namespace Basket.Host.Services
{
    public class BasketService : IBasketService
    {
        private readonly ICacheService _cacheService;
        private readonly ILogger<BasketService> _logger;
        private readonly IInternalHttpClientService _httpClient;
        private readonly Config _config;

        public BasketService(
            ICacheService cacheService,
            ILogger<BasketService> logger,
            IInternalHttpClientService httpClientService,
            IOptions<Config> options)
        {
            _cacheService = cacheService;
            _logger = logger;
            _httpClient = httpClientService;
            _config = options.Value;
        }

        public async Task AddToBasket(string userId, int itemId, string name, decimal cost)
        {
            var result = await _cacheService.GetAsync<BasketModel>(userId);
            if (result == null)
            {
                _logger.LogWarning("Basket is empty");
                result = new BasketModel();
            }

            result.BasketList.Add(new BasketItem()
            {
                Id = itemId,
                Name = name,
                Cost = cost
            });
            result.TotalCost = 0;
            foreach (var item in result.BasketList)
            {
                result.TotalCost += item.Cost;
            }

            await _cacheService.AddOrUpdateAsync(userId, result);
            _logger.LogInformation(LoggerDefaultResponse.SuccessfulUpdate);
        }

        public async Task RemoveFromBasket(string userId, int itemId)
        {
            var basket = await _cacheService.GetAsync<BasketModel>(userId);
            if (basket == null)
            {
                _logger.LogError($"Value with key: {userId} — {LoggerDefaultResponse.NotFound}");
                return;
            }

            var basketItem = basket.BasketList.FirstOrDefault(f => f.Id == itemId);
            if (basketItem == default)
            {
                _logger.LogError($"Value with key: {userId} ItemId: {itemId} — {LoggerDefaultResponse.NotFound}");
                return;
            }

            basket.BasketList.Remove(basketItem);
            await _cacheService.AddOrUpdateAsync(userId, basket);
            _logger.LogInformation(LoggerDefaultResponse.SuccessfulDelete);
        }

        public async Task MakeAnOrder(string userId)
        {
            var basket = await _cacheService.GetAsync<BasketModel>(userId);
            if (basket == null)
            {
                _logger.LogError($"the Order was {LoggerDefaultResponse.NotFound}");
                return;
            }

            await _httpClient.SendAsync<object, BasketModel>(
                $"{_config.OrderApi}/order/add",
                HttpMethod.Post,
                new UserBasket()
                {
                    UserId = userId,
                    BasketList = basket.BasketList
                });

            await RemoveCache(userId);
            _logger.LogInformation("the Сache has been cleared");
        }

        public async Task<BasketModel> GetBasket(string key)
        {
            var result = await _cacheService.GetAsync<BasketModel>(key);
            if (result == null)
            {
                _logger.LogError(LoggerDefaultResponse.NotFound);
                result = new BasketModel();
            }

            return result;
        }

        private async Task RemoveCache(string userId)
        {
            await _cacheService.Remove(userId);
        }
    }
}