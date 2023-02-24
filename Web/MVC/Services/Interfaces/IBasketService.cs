using MVC.ViewModels;

public interface IBasketService
{
    public Task AddToBasket(BasketItem item);
    public Task RemoveFromBasket(string userId, int itemid);
    public Task<Basket> GetBasket(string userId);
    public Task MakeAnOrder(string userId);
}