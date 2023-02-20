namespace Order.Host.Models.Request.Update
{
    public class PaginatedUserOrdersRequest : UserIdRequest
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
