namespace Catalog.Host.Models.Request
{
    public class BaseRequest
    {
        [Range(1, int.MaxValue)]
        public int Id { get; set; }
    }
}
