using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Request;
using Catalog.Host.Models.Request.AddRequests;
using Catalog.Host.Models.Request.UpdateRequest;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Controllers
{
    [ApiController]
    [Scope("catalog.api.frequence")]
    [Authorize(Policy = AuthPolicy.AllowClientPolicy)]
    [Route(ComponentDefaults.DefaultRoute)]
    public class FrequencyController : ControllerBase
    {
        private readonly ILogger<FrequencyController> _logger;
        private readonly IFrequencyService _service;

        public FrequencyController(
            ILogger<FrequencyController> logger,
            IFrequencyService service)
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
        [ProducesResponseType(typeof(Frequency), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(BaseRequest request)
        {
            var result = await _service.Get(request.Id);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Frequency), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateMeet(UpdateStringRequest request)
        {
            var result = await _service.UpdateMeet(request.Id, request.UpdateValue);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Frequency), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete(BaseRequest request)
        {
            var result = await _service.Delete(request.Id);
            return Ok(result);
        }
    }
}
