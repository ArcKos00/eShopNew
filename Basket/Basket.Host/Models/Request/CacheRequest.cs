namespace Basket.Host.Models.Request
{
    public class CacheRequest
    {
        [Required]
        public string UserId { get; set; } = null!;
    }
}
