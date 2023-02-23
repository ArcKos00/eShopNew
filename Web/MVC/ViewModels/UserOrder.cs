namespace MVC.ViewModels
{
    public class UserOrder
    {
        public long OrdersCount { get; set; }
        public List<Order> Orders { get; set; } = new List<Order>();
    }
}
