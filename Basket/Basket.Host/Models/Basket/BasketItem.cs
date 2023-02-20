namespace Basket.Host.Models.Basket
{
    public class BasketItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Cost { get; set; }
    }
}
