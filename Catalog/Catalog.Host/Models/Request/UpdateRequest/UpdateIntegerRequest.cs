namespace Catalog.Host.Models.Request.UpdateRequest
{
    public class UpdateIntegerRequest : BaseRequest
    {
        [Range(1, int.MaxValue)]
        public int UpdateValue { get; set; }
    }
}
