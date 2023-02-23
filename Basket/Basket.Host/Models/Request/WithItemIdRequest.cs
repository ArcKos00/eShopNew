namespace Basket.Host.Models.Request
{
    public class WithItemIdRequest : CacheRequest
    {
        [Range(0, int.MaxValue)]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;
        [Range(0, double.MaxValue)]
        public decimal Cost { get; set; }
    }
}
