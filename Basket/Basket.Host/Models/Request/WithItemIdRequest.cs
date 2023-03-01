namespace Basket.Host.Models.Request
{
    public class WithItemIdRequest : CacheRequest
    {
        [Range(0, int.MaxValue)]
        public int Id { get; set; }
    }
}
