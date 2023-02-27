using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Enums;
using Catalog.Host.Models.Request;
using Catalog.Host.Models.Response;
using Catalog.Host.Services.Interfaces;
namespace Catalog.Host.Controllers
{
    [ApiController]
    [Scope("catalog.api.catalogbff")]
    [AllowAnonymous]
    [Authorize(Policy = AuthPolicy.AllowEndUserPolicy)]
    [Route(ComponentDefaults.DefaultRoute)]
    public class CatalogBffController : ControllerBase
    {
        private readonly ILogger<CatalogBffController> _logger;
        private readonly IAbnormalTypeService _typeService;
        private readonly IAnomalyService _anomalyService;
        private readonly IArtefactService _service;
        private readonly IFrequencyService _frequencyService;

        public CatalogBffController(
            ILogger<CatalogBffController> logger,
            IAbnormalTypeService typeService,
            IAnomalyService anomalyService,
            IArtefactService service,
            IFrequencyService frequencyService)
        {
            _logger = logger;
            _typeService = typeService;
            _anomalyService = anomalyService;
            _service = service;
            _frequencyService = frequencyService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(Artefact), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Item(BaseRequest request)
        {
            var result = await _service.Get(request.Id);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Artefact), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetFullItem(BaseRequest request)
        {
            var result = await _service.GetWithContent(request.Id);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(PaginatedItemsResponse<Artefact>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Items(PaginatedItemRequest<TypeFilter> request)
        {
            var result = await _service.GetPage(request.PageIndex, request.PageSize, request.Filter);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(List<Anomaly>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAnomalies()
        {
            var result = await _anomalyService.GetAnomaly();
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(List<Frequency>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetMeets()
        {
            var result = await _frequencyService.GetMeets();
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(List<AbnormalType>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetTypes()
        {
            var result = await _typeService.GetTypes();
            return Ok(result);
        }
    }
}