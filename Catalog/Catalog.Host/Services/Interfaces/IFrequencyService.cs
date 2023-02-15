using Catalog.Host.Models.Dtos;

namespace Catalog.Host.Services.Interfaces
{
    public interface IFrequencyService
    {
        public Task<int?> Add(string name);
        public Task<Frequency> Get(int id);
        public Task<List<Frequency>> GetMeets();
        public Task<bool> UpdateMeet(int id, string meet);
        public Task<bool> Delete(int id);
    }
}
