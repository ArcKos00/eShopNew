namespace Order.Host.Models.Request.Update
{
    public class PaginatedRequest
    {
        [Range(0, int.MaxValue)]
        public int PageIndex { get; set; }
        [Range(0, int.MaxValue)]
        public int PageSize { get; set; }
    }
}
