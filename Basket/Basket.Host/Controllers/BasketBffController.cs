using Basket.Host.Models.Basket;
using Basket.Host.Models.Request;
using Basket.Host.Services.Interfaces;
using Infrastructure.Identity;

namespace Basket.Host.Controllers
{
    // [Authorize(Policy = AuthPolicy.AllowEndUserPolicy)]
    [ApiController]
    [Route(ComponentDefaults.DefaultRoute)]
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
        [AllowAnonymous]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddToBasket(WithItemIdRequest request)
        {
            await _service.AddToBasket(request.UserId, request.ItemId, request.Name, request.Cost);
            return Ok();
        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> RemoveFromBasket(WithItemIdRequest request)
        {
            await _service.RemoveFromBasket(request.UserId, request.ItemId);
            return Ok();
        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> MakeAnOrder(CacheRequest request)
        {
            await _service.MakeAnOrder(request.UserId);
            return Ok();
        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(BasketModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetBasket(CacheRequest request)
        {
            var result = await _service.GetBasket(request.UserId);
            return Ok(result);
        }
    }
}
