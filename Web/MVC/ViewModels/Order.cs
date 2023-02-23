namespace MVC.ViewModels
{
    public class Order
    {
        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public string Status { get; set; } = null!;
        public DateTime Date { get; set; }
        public List<OrderItem> Items { get; set; } = new List<OrderItem>();
        public decimal TotalCount { get; set; }
    }
}
