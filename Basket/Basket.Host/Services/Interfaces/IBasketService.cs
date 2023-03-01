using Basket.Host.Models.Basket;

namespace Basket.Host.Services.Interfaces
{
    public interface IBasketService
    {
        public Task<bool> AddToBasket(string userId, int itemId, string name, decimal cost);
        public Task<bool> RemoveFromBasket(string userId, int itemId);
        public Task<bool> MakeAnOrder(string userId);
        public Task<BasketModel> GetBasket(string userId);
        public Task<bool> Clear(string userId);
    }
}
