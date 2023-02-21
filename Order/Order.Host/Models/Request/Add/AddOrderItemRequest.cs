namespace Order.Host.Models.Request.Add
{
    public class AddOrderItemRequest
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;
        [Range(0, double.MaxValue)]
        public decimal Cost { get; set; }
        [Range(1, int.MinValue)]
        public int OrderId { get; set; }
    }
}
