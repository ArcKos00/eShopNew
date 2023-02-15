using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Request;
using Catalog.Host.Models.Request.AddRequests;
using Catalog.Host.Models.Request.UpdateRequest;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Controllers
{
    [ApiController]
    [Scope("catalog.api.artifact")]
    [Authorize(Policy = AuthPolicy.AllowClientPolicy)]
    [Route(ComponentDefaults.DefaultRoute)]
    public class ArtefactController : ControllerBase
    {
        private readonly ILogger<ArtefactController> _logger;
        private readonly IArtefactService _service;

        public ArtefactController(
            ILogger<ArtefactController> logger,
            IArtefactService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpPost]
        [ProducesResponseType(typeof(int?), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Add(AddArtefactRequest request)
        {
            var result = await _service.Add(request.Name, request.Cost, request.ImagePath, request.Nature, request.AnomalyId, request.TypeId, request.FrequencyId, request.CharacteristicId);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Artefact), (int)HttpStatusCode.OK)]
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
        public async Task<IActionResult> UpdateNature(UpdateStringRequest request)
        {
            var result = await _service.UpdateNature(request.Id, request.UpdateValue);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateImage(UpdateStringRequest request)
        {
            var result = await _service.UpdateImage(request.Id, request.UpdateValue);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateCost(UpdatePriceRequest request)
        {
            var result = await _service.UpdateCost(request.Id, request.Price);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete(BaseRequest request)
        {
            var result = await _service.Delete(request.Id);
            return Ok(result);
        }
    }
}
