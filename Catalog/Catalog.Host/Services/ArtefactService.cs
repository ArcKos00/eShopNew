using AutoMapper;
using Catalog.Host.Data;
using Catalog.Host.Repositories.Interfaces;
using Infrastructure.Services.Interfaces;
using Infrastructure.Services;
using Catalog.Host.Services.Interfaces;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Enums;
using Catalog.Host.Models.Response;

namespace Catalog.Host.Services
{
    public class ArtefactService : BaseDataService<ApplicationDbContext>, IArtefactService
    {
        private readonly IArtefactRepository _repository;
        private readonly ILogger<ArtefactService> _logger;
        private readonly IMapper _mapper;

        public ArtefactService(
            IArtefactRepository repository,
            ILogger<ArtefactService> logger,
            IMapper mapper,
            ILogger<BaseDataService<ApplicationDbContext>> baseLogger,
            IDbContextWrapper<ApplicationDbContext> wrapper)
            : base(wrapper, baseLogger)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<int?> Add(string name, decimal cost, string img, string nature, int anomalyId, int abnormalTypeId, int frequencyId, int characteristicId)
        {
            return await ExecuteSafeAsync(async () =>
            {
                return await _repository.Add(name, cost, img, nature, anomalyId, abnormalTypeId, frequencyId, characteristicId);
            });
        }

        public async Task<Artefact> Get(int id)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var result = await _repository.Get(id);
                if (result == null)
                {
                    _logger.LogError(LoggerDefaultResponse.NotFound);
                    return new Artefact();
                }

                return _mapper.Map<Artefact>(result);
            });
        }

        public async Task<PaginatedItemsResponse<Artefact>> GetPage(int pageIndex, int pageSize, Dictionary<TypeFilter, int>? filters)
        {
            return await ExecuteSafeAsync(async () =>
            {
                int? meetFilter = null;
                int? anomalyFilter = null;
                int? abnormalFilter = null;
                if (filters != null)
                {
                    if (filters.TryGetValue(TypeFilter.Meets, out var meet))
                    {
                        meetFilter = meet;
                    }

                    if (filters.TryGetValue(TypeFilter.Anomaly, out var anomaly))
                    {
                        anomalyFilter = anomaly;
                    }

                    if (filters.TryGetValue(TypeFilter.Type, out var abnormal))
                    {
                        abnormalFilter = abnormal;
                    }
                }

                var result = await _repository.GetPage(pageIndex, pageSize, anomalyFilter, abnormalFilter, meetFilter);
                if (result == null)
                {
                    _logger.LogError(LoggerDefaultResponse.NotFound);
                    return new PaginatedItemsResponse<Artefact>();
                }

                return new PaginatedItemsResponse<Artefact>()
                {
                    Count = result.TotalCount,
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    Data = result.Data.Select(s => _mapper.Map<Artefact>(s)).ToList()
                };
            });
        }

        public async Task<Artefact> GetWithContent(int id)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var result = await _repository.GetWithContent(id);
                if (result == null)
                {
                    _logger.LogError(LoggerDefaultResponse.NotFound);
                    return new Artefact();
                }

                return _mapper.Map<Artefact>(result);
            });
        }

        public async Task<bool> UpdateCost(int id, decimal cost)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var result = await _repository.UpdateCost(id, cost);
                if (!result)
                {
                    _logger.LogError(LoggerDefaultResponse.FailedUpdate);
                    return false;
                }

                _logger.LogInformation(LoggerDefaultResponse.SuccessfulUpdate);
                return true;
            });
        }

        public Task<bool> UpdateImage(int id, string image)
        {
            return ExecuteSafeAsync(async () =>
            {
                var result = await _repository.UpdateImage(id, image);
                if (!result)
                {
                    _logger.LogError(LoggerDefaultResponse.FailedUpdate);
                    return false;
                }

                _logger.LogInformation(LoggerDefaultResponse.SuccessfulUpdate);
                return true;
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

        public async Task<bool> UpdateNature(int id, string name)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var result = await _repository.UpdateNature(id, name);
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
