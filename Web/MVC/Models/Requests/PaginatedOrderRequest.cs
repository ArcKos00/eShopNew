namespace MVC.Models.Requests
{
    public class PaginatedOrderRequest : PaginatedBaseRequest
    {
        public string UserId { get; set; } = null!;
    }
}
