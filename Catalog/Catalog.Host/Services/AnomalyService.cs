using Catalog.Host.Data;
using Catalog.Host.Repositories.Interfaces;
using Infrastructure.Services.Interfaces;
using Infrastructure.Services;
using Catalog.Host.Services.Interfaces;
using Catalog.Host.Models.Dtos;

namespace Catalog.Host.Services
{
    public class AnomalyService : BaseDataService<ApplicationDbContext>, IAnomalyService
    {
        private readonly IAnomalyRepository _repository;
        private readonly ILogger<AnomalyService> _logger;
        private readonly IMapper _mapper;

        public AnomalyService(
            IAnomalyRepository repository,
            ILogger<AnomalyService> logger,
            IMapper mapper,
            ILogger<BaseDataService<ApplicationDbContext>> baseLogger,
            IDbContextWrapper<ApplicationDbContext> wrapper)
            : base(wrapper, baseLogger)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<int?> Add(string name, int abnormalTypeId, int locationPlaceId, int frequenceId)
        {
            return await ExecuteSafeAsync(async () =>
            {
                return await _repository.Add(name, abnormalTypeId, locationPlaceId, frequenceId);
            });
        }

        public async Task<Anomaly> Get(int id)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var result = await _repository.Get(id);
                if (result == null)
                {
                    _logger.LogError(LoggerDefaultResponse.NotFound);
                    return new Anomaly();
                }

                return _mapper.Map<Anomaly>(result);
            });
        }

        public async Task<List<Anomaly>> GetAnomaly()
        {
            return await ExecuteSafeAsync(async () =>
            {
                var result = await _repository.GetAnomaly();
                if (result == null)
                {
                    _logger.LogError(LoggerDefaultResponse.NotFound);
                    return new List<Anomaly>();
                }

                return result.Select(s => _mapper.Map<Anomaly>(s)).ToList();
            });
        }

        public async Task<bool> UpdateName(int id, string name)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var result = await _repository.UpdateName(id, name);
                if (!result)
                {
                    _logger.LogError(LoggerDefaultResponse.FailedUpdate);
                    return false;
                }

                _logger.LogInformation(LoggerDefaultResponse.SuccessfulUpdate);
                return true;
            });
        }

        public async Task<bool> Delete(int id)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var result = await _repository.Delete(id);
                if (!result)
                {
                    _logger.LogError(LoggerDefaultResponse.FailedDelete);
                    return false;
                }

                _logger.LogInformation(LoggerDefaultResponse.SuccessfulDelete);
                return true;
            });
        }
    }
}
