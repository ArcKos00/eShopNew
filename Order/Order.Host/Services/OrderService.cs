using AutoMapper;
using Infrastructure.Services.Interfaces;
using Order.Host.Data;
using Order.Host.Data.Entities;
using Order.Host.Models.Dto;
using Order.Host.Models.Response;
using Order.Host.Repositories.Interfaces;
using Order.Host.Services.Interfaces;

namespace Order.Host.Services
{
    public class OrderService : BaseDataService<ApplicationDbContext>, IOrderService
    {
        private readonly IOrderRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<OrderService> _logger;

        public OrderService(
            IDbContextWrapper<ApplicationDbContext> wrapper,
            ILogger<BaseDataService<ApplicationDbContext>> baseLogger,
            IOrderRepository repository,
            IMapper mapper,
            ILogger<OrderService> logger)
            : base(wrapper, baseLogger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<int?> Add(string userId, List<OrderItem> items)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var total = 0m;
                foreach (var item in items)
                {
                    total += item.Cost;
                }

                var result = await _repository.Add(
                    userId,
                    total,
                    items.Select(s => _mapper.Map<OrderItemEntity>(s)).ToList());
                if (result == null || result == 0)
                {
                    _logger.LogError(LoggerDefaultResponse.FailedCreate);
                    return 0;
                }

                _logger.LogInformation(LoggerDefaultResponse.SuccessfulCreate);
                return result;
            });
        }

        public async Task<bool> Delete(int id)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var result = await _repository.Delete(id);
                if (!result)
                {
                    _logger.LogError(LoggerDefaultResponse.FailedDelete);
                    return false;
                }

                _logger.LogInformation(LoggerDefaultResponse.SuccessfulDelete);
                return true;
            });
        }

        public async Task<Orders> Get(int id)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var result = await _repository.Get(id);
                if (result == null)
                {
                    _logger.LogError(LoggerDefaultResponse.NotFound);
                    return new Orders();
                }

                return _mapper.Map<Orders>(result);
            });
        }

        public async Task<UserOrders<Orders>> GetUserOrders(string userId, int pageIndex, int pageSize)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var result = await _repository.GetUserOrders(userId, pageIndex, pageSize);
                if (result == null)
                {
                    _logger.LogError(LoggerDefaultResponse.NotFound);
                    return new UserOrders<Orders>();
                }

                return new UserOrders<Orders>
                {
                    OrdersCount = result.OrdersCount,
                    Orders = result.Orders.Select(s => _mapper.Map<Orders>(s)).ToList()
                };
            });
        }

        public async Task<bool> UpdateStatus(int id, string status)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var result = await _repository.UpdateStatus(id, status);
                if (!result)
                {
                    _logger.LogError(LoggerDefaultResponse.FailedUpdate);
                    return false;
                }

                _logger.LogInformation(LoggerDefaultResponse.SuccessfulUpdate);
                return true;
            });
        }
    }
}
