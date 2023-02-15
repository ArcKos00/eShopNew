using AutoMapper;
using Catalog.Host.Data;
using Catalog.Host.Repositories.Interfaces;
using Infrastructure.Services.Interfaces;
using Infrastructure.Services;
using Catalog.Host.Services.Interfaces;
using Catalog.Host.Models.Dtos;

namespace Catalog.Host.Services
{
    public class FrequencyService : BaseDataService<ApplicationDbContext>, IFrequencyService
    {
        private readonly IFrequencyRepository _repository;
        private readonly ILogger<FrequencyService> _logger;
        private readonly IMapper _mapper;

        public FrequencyService(
            IFrequencyRepository repository,
            ILogger<FrequencyService> logger,
            IMapper mapper,
            ILogger<BaseDataService<ApplicationDbContext>> baseLogger,
            IDbContextWrapper<ApplicationDbContext> wrapper)
            : base(wrapper, baseLogger)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<int?> Add(string meet)
        {
            return await ExecuteSafeAsync(async () =>
            {
                return await _repository.Add(meet);
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

        public async Task<Frequency> Get(int id)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var result = await _repository.Get(id);
                if (result == null)
                {
                    _logger.LogError(LoggerDefaultResponse.NotFound);
                    return new Frequency();
                }

                return _mapper.Map<Frequency>(result);
            });
        }

        public async Task<List<Frequency>> GetMeets()
        {
            return await ExecuteSafeAsync(async () =>
            {
                var result = await _repository.GetMeets();
                if (result == null)
                {
                    _logger.LogError(LoggerDefaultResponse.NotFound);
                    return new List<Frequency>();
                }

                return result.Select(s => _mapper.Map<Frequency>(s)).ToList();
            });
        }

        public async Task<bool> UpdateMeet(int id, string name)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var result = await _repository.UpdateMeet(id, name);
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
