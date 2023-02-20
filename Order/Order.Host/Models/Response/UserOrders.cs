namespace Order.Host.Models.Response
{
    public class UserOrders<T>
    {
        public long OrdersCount { get; set; }
        public List<T> Orders { get; set; } = new List<T>();
    }
}
