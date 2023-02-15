using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Request;
using Catalog.Host.Models.Request.AddRequests;
using Catalog.Host.Models.Request.UpdateRequest;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Controllers
{
    [ApiController]
    [Scope("catalog.api.location")]
    [Authorize(Policy = AuthPolicy.AllowClientPolicy)]
    [Route(ComponentDefaults.DefaultRoute)]
    public class LocationController : ControllerBase
    {
        private readonly ILogger<LocationController> _logger;
        private readonly ILocationService _service;

        public LocationController(
            ILogger<LocationController> logger,
            ILocationService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpPost]
        [ProducesResponseType(typeof(int?), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Add(AddSimpleTypeRequest request)
        {
            var result = await _service.Add(request.Name);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Location), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(BaseRequest request)
        {
            var result = await _service.Get(request.Id);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Location), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdatePlace(UpdateStringRequest request)
        {
            await _service.UpdatePlace(request.Id, request.UpdateValue);
            return Ok();
        }

        [HttpPost]
        [ProducesResponseType(typeof(Location), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete(BaseRequest request)
        {
            await _service.Delete(request.Id);
            return Ok();
        }
    }
}
