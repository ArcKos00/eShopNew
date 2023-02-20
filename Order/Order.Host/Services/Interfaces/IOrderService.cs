using Order.Host.Models.Dto;
using Order.Host.Models.Response;

namespace Order.Host.Services.Interfaces
{
    public interface IOrderService
    {
        public Task<int?> Add(string userId, List<OrderItem> items);
        public Task<Orders> Get(int id);
        public Task<UserOrders<Orders>> GetUserOrders(string userId, int pageIndex, int pageSize);
        public Task<bool> UpdateStatus(int id, string status);
        public Task<bool> Delete(int id);
    }
}
