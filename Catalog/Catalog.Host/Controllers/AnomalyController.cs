using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Request;
using Catalog.Host.Models.Request.AddRequests;
using Catalog.Host.Models.Request.UpdateRequest;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Controllers
{
    [ApiController]
    [Scope("catalog.api.anomaly")]
    [Authorize(Policy = AuthPolicy.AllowClientPolicy)]
    [Route(ComponentDefaults.DefaultRoute)]
    public class AnomalyController : ControllerBase
    {
        private readonly IAnomalyService _service;
        private readonly ILogger<AnomalyController> _logger;

        public AnomalyController(
            IAnomalyService service,
            ILogger<AnomalyController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(typeof(int?), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Add(AddAnomalyRequest request)
        {
            var result = await _service.Add(request.Name, request.TypeId, request.LocationId, request.FrequencyId);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Anomaly), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(BaseRequest request)
        {
            var result = await _service.Get(request.Id);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateName(UpdateStringRequest request)
        {
            await _service.UpdateName(request.Id, request.UpdateValue);
            return Ok();
        }

        [HttpPost]
        [ProducesResponseType(typeof(int?), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete(BaseRequest request)
        {
            await _service.Delete(request.Id);
            return Ok();
        }
    }
}
