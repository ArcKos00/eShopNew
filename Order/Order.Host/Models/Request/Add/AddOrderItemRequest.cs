namespace Order.Host.Models.Request.Add
{
    public class AddOrderItemRequest
    {
        public string Name { get; set; } = null!;
        public decimal Cost { get; set; }
        public int OrderId { get; set; }
    }
}
