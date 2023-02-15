namespace Catalog.Host.Models.Request.UpdateRequest
{
    public class UpdatePriceRequest : BaseRequest
    {
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }
    }
}
