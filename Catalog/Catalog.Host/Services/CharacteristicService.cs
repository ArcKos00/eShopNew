using Catalog.Host.Data;
using Catalog.Host.Repositories.Interfaces;
using Infrastructure.Services.Interfaces;
using Infrastructure.Services;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Services
{
    public class CharacteristicService : BaseDataService<ApplicationDbContext>, ICharacteristicService
    {
        private readonly ICharacteristicRepository _repository;
        private readonly ILogger<CharacteristicService> _logger;
        private readonly IMapper _mapper;

        public CharacteristicService(
            ICharacteristicRepository repository,
            ILogger<CharacteristicService> logger,
            IMapper mapper,
            ILogger<BaseDataService<ApplicationDbContext>> baseLogger,
            IDbContextWrapper<ApplicationDbContext> wrapper)
            : base(wrapper, baseLogger)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<int?> Add(int radiation, int restoration, int health, int woundHealing, int maximumWeight, int protectionDogs, int thermalProtection, int chemicalProtection, int electricalProtection, int saturation)
        {
            return await ExecuteSafeAsync(async () =>
            {
                return await _repository.Add(radiation, restoration, health, woundHealing, maximumWeight, protectionDogs, thermalProtection, chemicalProtection, electricalProtection, saturation);
            });
        }

        public async Task<bool> UpdateChenmicalProtection(int id, int value)
        {
            return await Update(_repository.UpdateChenmicalProtection, id, value);
        }

        public async Task<bool> UpdateElectricalProtection(int id, int value)
        {
            return await Update(_repository.UpdateElectricalProtection, id, value);
        }

        public async Task<bool> UpdateHealth(int id, int value)
        {
            return await Update(_repository.UpdateHealth, id, value);
        }

        public async Task<bool> UpdateMaximumWeight(int id, int value)
        {
            return await Update(_repository.UpdateMaximumWeight, id, value);
        }

        public async Task<bool> UpdateProtecrionDogs(int id, int value)
        {
            return await Update(_repository.UpdateProtecrionDogs, id, value);
        }

        public async Task<bool> UpdateRadiation(int id, int value)
        {
            return await Update(_repository.UpdateRadiation, id, value);
        }

        public async Task<bool> UpdateRestoration(int id, int value)
        {
            return await Update(_repository.UpdateRestoration, id, value);
        }

        public async Task<bool> UpdateSaturation(int id, int value)
        {
            return await Update(_repository.UpdateSaturation, id, value);
        }

        public async Task<bool> UpdateThermalProtection(int id, int value)
        {
            return await Update(_repository.UpdateThermalProtection, id, value);
        }

        public async Task<bool> UpdateWoundHealing(int id, int value)
        {
            return await Update(_repository.UpdateWoundHealing, id, value);
        }

        public async Task<bool> Delete(int id)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var result = await _repository.Delete(id);
                if (!result)
                {
                    _logger.LogError(LoggerDefaultResponse.FailedDelete);
                    return result;
                }

                _logger.LogInformation(LoggerDefaultResponse.SuccessfulDelete);
                return result;
            });
        }

        public async Task<Characteristic> Get(int id)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var result = await _repository.Get(id);
                if (result == null)
                {
                    return new Characteristic();
                }

                return _mapper.Map<Characteristic>(result);
            });
        }

        private async Task<bool> Update(Func<int, int, Task<bool>> func, int id, int value)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var result = await func(id, value);
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
