using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Interfaces;
using Infrastructure.Services.Interfaces;

namespace Catalog.Host.Repositories
{
    public class CharacteristicRepository : ICharacteristicRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CharacteristicRepository> _logger;

        public CharacteristicRepository(IDbContextWrapper<ApplicationDbContext> wrapper, ILogger<CharacteristicRepository> logger)
        {
            _context = wrapper.DbContext;
            _logger = logger;
        }

        public async Task<int?> Add(int radiation, int restoration, int restorationhealth, int woundHealing, int maximumWeight, int protectionDogs, int thermalProtection, int chemicalProtection, int electricalProtection, int saturation)
        {
            var entity = await _context.Characteristic.AddAsync(new CharacteristicEntity()
            {
                Radiation = radiation,
                Restoration = restoration,
                WoundHealing = woundHealing,
                MaximumWeight = maximumWeight,
                ProtectionDogs = protectionDogs,
                ThermalProtection = thermalProtection,
                ElectricalProtection = chemicalProtection,
                Saturation = saturation,
                ChemicalProtection = chemicalProtection,
            });

            await _context.SaveChangesAsync();
            return entity.Entity.Id;
        }

        public async Task<bool> UpdateChenmicalProtection(int id, int value)
        {
            var entity = await Get(id);
            if (entity == null)
            {
                _logger.LogError(LoggerDefaultResponse.FailedUpdate);
                return false;
            }

            entity.ChemicalProtection = value;
            _context.Entry(entity).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateElectricalProtection(int id, int value)
        {
            var entity = await Get(id);
            if (entity == null)
            {
                _logger.LogError(LoggerDefaultResponse.FailedUpdate);
                return false;
            }

            entity.ElectricalProtection = value;
            _context.Entry(entity).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateHealth(int id, int value)
        {
            var entity = await Get(id);
            if (entity == null)
            {
                _logger.LogError(LoggerDefaultResponse.FailedUpdate);
                return false;
            }

            entity.RestorationHealth = value;
            _context.Entry(entity).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateMaximumWeight(int id, int value)
        {
            var entity = await Get(id);
            if (entity == null)
            {
                _logger.LogError(LoggerDefaultResponse.FailedUpdate);
                return false;
            }

            entity.MaximumWeight = value;
            _context.Entry(entity).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateProtecrionDogs(int id, int value)
        {
            var entity = await Get(id);
            if (entity == null)
            {
                _logger.LogError(LoggerDefaultResponse.FailedUpdate);
                return false;
            }

            entity.ProtectionDogs = value;
            _context.Entry(entity).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateRadiation(int id, int value)
        {
            var entity = await Get(id);
            if (entity == null)
            {
                _logger.LogError(LoggerDefaultResponse.FailedUpdate);
                return false;
            }

            entity.Radiation = value;
            _context.Entry(entity).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateRestoration(int id, int value)
        {
            var entity = await Get(id);
            if (entity == null)
            {
                _logger.LogError(LoggerDefaultResponse.FailedUpdate);
                return false;
            }

            entity.Restoration = value;
            _context.Entry(entity).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateSaturation(int id, int value)
        {
            var entity = await Get(id);
            if (entity == null)
            {
                _logger.LogError(LoggerDefaultResponse.FailedUpdate);
                return false;
            }

            entity.Saturation = value;
            _context.Entry(entity).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateThermalProtection(int id, int value)
        {
            var entity = await Get(id);
            if (entity == null)
            {
                _logger.LogError(LoggerDefaultResponse.FailedUpdate);
                return false;
            }

            entity.ThermalProtection = value;
            _context.Entry(entity).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateWoundHealing(int id, int value)
        {
            var entity = await Get(id);
            if (entity == null)
            {
                _logger.LogError(LoggerDefaultResponse.FailedUpdate);
                return false;
            }

            entity.WoundHealing = value;
            _context.Entry(entity).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(int id)
        {
            var entity = await Get(id);
            if (entity == null)
            {
                _logger.LogError(LoggerDefaultResponse.FailedDelete);
                return false;
            }

            _context.Entry(entity).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<CharacteristicEntity?> Get(int id)
        {
            return await _context.Characteristic.FirstOrDefaultAsync(f => f.Id == id);
        }
    }
}
