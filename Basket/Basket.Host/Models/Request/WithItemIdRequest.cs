namespace Basket.Host.Models.Request
{
    public class WithItemIdRequest : CacheRequest
    {
        public int ItemId { get; set; }
        public string Name { get; set; } = null!;
        public decimal Cost { get; set; }
    }
}
