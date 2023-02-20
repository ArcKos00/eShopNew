namespace Order.Host.Models.Request
{
    public class UpdateStatusRequest : ItemIdRequest
    {
        public string Status { get; set; } = null!;
    }
}
