using MVC.Services.Interfaces;
using MVC.ViewModels;

namespace MVC.Services
{
    public class BasketService
    {
        private readonly IOptions<AppSettings> _options;
        private readonly IHttpClientService _httpClient;
        private readonly ILogger<BasketService> _logger;

        public BasketService(
            IHttpClientService client,
            ILogger<BasketService> logger,
            IOptions<AppSettings> options)
        {
            _httpClient= client;
            _logger= logger;
            _options= options;
        }

        public async Task AddToBasket(BasketItem item)
        {
            await _httpClient.SendAsync<Basket, BasketItem>($"{_options.Value.BasketUrl}/addtobasket", HttpMethod.Post, item);
        }

        public async Task RemoveFromBasket(string userId, int itemid)
        {
            await _httpClient.SendAsync<object, object>($"{_options.Value.BasketUrl}/removefrombasket", HttpMethod.Post, new {UserId = userId, Id = itemid});
        }
        public async Task<Basket> GetBasket(string userId)
        {
            return await _httpClient.SendAsync<Basket, object>($"{_options.Value.BasketUrl}/getbasket", HttpMethod.Post, new { UserId = userId});
        }

        public async Task MakeAnOrder(string userId)
        {
            await _httpClient.SendAsync<object, object>($"{_options.Value.BasketUrl}/makeanorder", HttpMethod.Post, new { UserId = userId });
        }
    }
}
