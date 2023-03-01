namespace Basket.Host.Models.Request
{
    public class WithItemRequest : WithItemIdRequest
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;
        [Range(0, double.MaxValue)]
        public decimal Cost { get; set; }
    }
}