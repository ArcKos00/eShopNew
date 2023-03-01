namespace Order.Host.Models.Dto
{
    public class Orders
    {
        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public string Status { get; set; } = null!;
        public DateTime Date { get; set; }
        public List<OrderItem> Items { get; set; } = new List<OrderItem>();
        public decimal TotalCost { get; set; }
    }
}
