using Catalog.Host.Models.Dtos;

namespace Catalog.Host.Services.Interfaces
{
    public interface ICharacteristicService
    {
        public Task<int?> Add(int radiation, int restoration, int health, int woundHealing, int maximumWeight, int protectionDogs, int thermalProtection, int chemicalProtection, int electricalProtection, int saturation);
        public Task<Characteristic> Get(int id);
        public Task<bool> UpdateRadiation(int id, int value);
        public Task<bool> UpdateRestoration(int id, int value);
        public Task<bool> UpdateHealth(int id, int value);
        public Task<bool> UpdateWoundHealing(int id, int value);
        public Task<bool> UpdateMaximumWeight(int id, int value);
        public Task<bool> UpdateProtecrionDogs(int id, int value);
        public Task<bool> UpdateThermalProtection(int id, int value);
        public Task<bool> UpdateChenmicalProtection(int id, int value);
        public Task<bool> UpdateElectricalProtection(int id, int value);
        public Task<bool> UpdateSaturation(int id, int value);
        public Task<bool> Delete(int id);
    }
}
