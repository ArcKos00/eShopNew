using Catalog.Host.Data;
using Catalog.Host.Repositories.Interfaces;
using Infrastructure.Services.Interfaces;
using Infrastructure.Services;
using Catalog.Host.Services.Interfaces;
using Catalog.Host.Models.Dtos;

namespace Catalog.Host.Services
{
    public class LocationService : BaseDataService<ApplicationDbContext>, ILocationService
    {
        private readonly ILocationRepository _repository;
        private readonly ILogger<LocationService> _logger;
        private readonly IMapper _mapper;

        public LocationService(
            ILocationRepository repository,
            ILogger<LocationService> logger,
            IMapper mapper,
            ILogger<BaseDataService<ApplicationDbContext>> baseLogger,
            IDbContextWrapper<ApplicationDbContext> wrapper)
            : base(wrapper, baseLogger)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<int?> Add(string name)
        {
            return await ExecuteSafeAsync(async () =>
            {
                return await _repository.Add(name);
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

        public async Task<Location> Get(int id)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var result = await _repository.Get(id);
                if (result == null)
                {
                    _logger.LogError(LoggerDefaultResponse.NotFound);
                    return new Location();
                }

                return _mapper.Map<Location>(result);
            });
        }

        public async Task<List<Location>> GetLocations()
        {
            return await ExecuteSafeAsync(async () =>
            {
                var result = await _repository.GetLocations();
                if (result == null)
                {
                    _logger.LogError(LoggerDefaultResponse.NotFound);
                    return new List<Location>();
                }

                return result.Select(s => _mapper.Map<Location>(s)).ToList();
            });
        }

        public async Task<bool> UpdatePlace(int id, string place)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var result = await _repository.UpdatePlace(id, place);
                if (!result)
                {
                    _logger.LogError(LoggerDefaultResponse.FailedUpdate);
                    return false;
                }

                _logger.LogInformation(LoggerDefaultResponse.SuccessfulUpdate);
                return true;
            });
        }
    }
}
