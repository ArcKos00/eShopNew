namespace MVC.ViewModels
{
    public class Basket
    {
        public int TotatCost { get; set; }
        public List<BasketItem> BAsketList { get; set; } = new List<BasketItem>();
    }
}
