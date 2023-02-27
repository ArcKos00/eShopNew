using Basket.Host.Models.Basket;
using Basket.Host.Models.Request;
using Basket.Host.Services.Interfaces;

namespace Basket.Host.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route(ComponentDefaults.DefaultRoute)]
    [Authorize(Policy = AuthPolicy.AllowEndUserPolicy)]
    [Scope("basket.basketCache.api")]
    public class BasketBffController : ControllerBase
    {
        private readonly IBasketService _service;
        private readonly ILogger<BasketBffController> _logger;

        public BasketBffController(
            IBasketService service,
            ILogger<BasketBffController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddToBasket(WithItemIdRequest request)
        {
            await _service.AddToBasket(request.UserId, request.Id, request.Name, request.Cost);
            return Ok();
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> RemoveFromBasket(WithItemIdRequest request)
        {
            await _service.RemoveFromBasket(request.UserId, request.Id);
            return Ok();
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> MakeAnOrder(CacheRequest request)
        {
            await _service.MakeAnOrder(request.UserId);
            return Ok();
        }

        [HttpPost]
        [ProducesResponseType(typeof(BasketModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetBasket(CacheRequest request)
        {
            var result = await _service.GetBasket(request.UserId);
            return Ok(result);
        }
    }
}
