using Basket.Host.Models.Basket;

namespace Basket.Host.Services.Interfaces
{
    public interface IBasketService
    {
        public Task AddToBasket(string userId, int itemId, string name, decimal cost);
        public Task RemoveFromBasket(string userId, int itemId);
        public Task MakeAnOrder(string userId);
        public Task<BasketModel> GetBasket(string key);
    }
}
