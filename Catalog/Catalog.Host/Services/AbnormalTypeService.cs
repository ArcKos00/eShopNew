using AutoMapper;
using Catalog.Host.Data;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;
using Infrastructure.Services;
using Infrastructure.Services.Interfaces;

namespace Catalog.Host.Services
{
    public class AbnormalTypeService : BaseDataService<ApplicationDbContext>, IAbnormalTypeService
    {
        private readonly IAbnormalTypeRepository _repository;
        private readonly ILogger<AbnormalTypeService> _logger;
        private readonly IMapper _mapper;

        public AbnormalTypeService(
            IAbnormalTypeRepository repository,
            ILogger<AbnormalTypeService> logger,
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
            return await ExecuteSafeAsync<int?>(async () =>
            {
                return await _repository.Add(name);
            });
        }

        public async Task<AbnormalType> Get(int id)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var result = await _repository.Get(id);
                if (result == null)
                {
                    _logger.LogError(LoggerDefaultResponse.NotFound);
                    return new AbnormalType();
                }

                return _mapper.Map<AbnormalType>(result);
            });
        }

        public async Task<List<AbnormalType>> GetTypes()
        {
            return await ExecuteSafeAsync(async () =>
            {
                var result = await _repository.GetTypes();
                if (result == null)
                {
                    _logger.LogError(LoggerDefaultResponse.NotFound);
                    return new List<AbnormalType>();
                }

                return result.Select(s => _mapper.Map<AbnormalType>(s)).ToList();
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
