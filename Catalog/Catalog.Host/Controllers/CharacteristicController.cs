using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Request;
using Catalog.Host.Models.Request.AddRequests;
using Catalog.Host.Models.Request.UpdateRequest;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Controllers
{
    [ApiController]
    [Scope("catalog.api.characteristic")]
    [Authorize(Policy = AuthPolicy.AllowClientPolicy)]
    [Route(ComponentDefaults.DefaultRoute)]
    public class CharacteristicController : ControllerBase
    {
        private readonly ILogger<CharacteristicController> _logger;
        private readonly ICharacteristicService _service;

        public CharacteristicController(
            ILogger<CharacteristicController> logger,
            ICharacteristicService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpPost]
        [ProducesResponseType(typeof(int?), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Add(AddCharacteristicRequest request)
        {
            var result = await _service.Add(
                request.Radiation,
                request.Restoration,
                request.RestorationHealth,
                request.WoundHealing,
                request.MaximumWeight,
                request.ProtectionDogs,
                request.ThermalProtection,
                request.ChemicalProtection,
                request.ElectricalProtection,
                request.Saturation);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Characteristic), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(BaseRequest request)
        {
            var result = await _service.Get(request.Id);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateRadiation(UpdateIntegerRequest request)
        {
            await _service.UpdateRadiation(request.Id, request.UpdateValue);
            return Ok();
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateRestoration(UpdateIntegerRequest request)
        {
            await _service.UpdateRestoration(request.Id, request.UpdateValue);
            return Ok();
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateHealth(UpdateIntegerRequest request)
        {
            await _service.UpdateHealth(request.Id, request.UpdateValue);
            return Ok();
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateWoundHealing(UpdateIntegerRequest request)
        {
            await _service.UpdateWoundHealing(request.Id, request.UpdateValue);
            return Ok();
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateMaximumWeight(UpdateIntegerRequest request)
        {
            await _service.UpdateMaximumWeight(request.Id, request.UpdateValue);
            return Ok();
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateProtecrionDogs(UpdateIntegerRequest request)
        {
            await _service.UpdateProtecrionDogs(request.Id, request.UpdateValue);
            return Ok();
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateThermalProtection(UpdateIntegerRequest request)
        {
            await _service.UpdateThermalProtection(request.Id, request.UpdateValue);
            return Ok();
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateChenmicalProtection(UpdateIntegerRequest request)
        {
            await _service.UpdateChenmicalProtection(request.Id, request.UpdateValue);
            return Ok();
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateElectricalProtection(UpdateIntegerRequest request)
        {
            await _service.UpdateElectricalProtection(request.Id, request.UpdateValue);
            return Ok();
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateSaturation(UpdateIntegerRequest request)
        {
            await _service.UpdateSaturation(request.Id, request.UpdateValue);
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
