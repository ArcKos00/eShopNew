namespace Basket.Host.Models.Request
{
    public class WithItemIdRequest
    {
        [Range(0, int.MaxValue)]
        public int Id { get; set; }
    }
}
