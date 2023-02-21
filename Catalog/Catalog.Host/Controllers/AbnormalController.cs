using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Request;
using Catalog.Host.Models.Request.AddRequests;
using Catalog.Host.Models.Request.UpdateRequest;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Controllers
{
    [ApiController]
    [Scope("catalog.api.abnormaltype")]
    [Authorize(Policy = AuthPolicy.AllowClientPolicy)]
    [Route(ComponentDefaults.DefaultRoute)]
    public class AbnormalController : ControllerBase
    {
        private readonly ILogger<AbnormalController> _logger;
        private readonly IAbnormalTypeService _service;

        public AbnormalController(
            ILogger<AbnormalController> logger,
            IAbnormalTypeService service)
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
        [ProducesResponseType(typeof(AbnormalType), (int)HttpStatusCode.OK)]
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
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete(BaseRequest request)
        {
            await _service.Delete(request.Id);
            return Ok();
        }
    }
}
