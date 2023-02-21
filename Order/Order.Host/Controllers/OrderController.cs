using Order.Host.Models.Dto;
using Order.Host.Models.Request;
using Order.Host.Models.Request.Add;
using Order.Host.Models.Request.Update;

namespace Order.Host.Controllers
{
    [ApiController]
    [Scope("order.order.api")]
    [Authorize(Policy = AuthPolicy.AllowClientPolicy)]
    [Route(ComponentDefaults.DefaultRoute)]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _service;
        private readonly ILogger<OrderController> _logger;

        public OrderController(
            IOrderService orderService,
            ILogger<OrderController> logger)
        {
            _service = orderService;
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(typeof(int?), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Add(AddOrderRequest request)
        {
            var result = await _service.Add(request.UserId, request.BasketList);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete(ItemIdRequest request)
        {
            await _service.Delete(request.Id);
            return Ok();
        }

        [HttpPost]
        [ProducesResponseType(typeof(Orders), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(ItemIdRequest request)
        {
            var result = await _service.Get(request.Id);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(int?), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetUserOrders(PaginatedUserOrdersRequest request)
        {
            var result = await _service.GetUserOrders(request.UserId, request.PageIndex, request.PageSize);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateStatus(UpdateStatusRequest request)
        {
            await _service.UpdateStatus(request.Id, request.Status);
            return Ok();
        }
    }
}
