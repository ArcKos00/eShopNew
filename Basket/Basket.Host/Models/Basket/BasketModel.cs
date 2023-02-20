namespace Basket.Host.Models.Basket
{
    public class BasketModel
    {
        public decimal TotalCost { get; set; }
        public List<BasketItem> BasketList { get; set; } = new List<BasketItem>();
    }
}
