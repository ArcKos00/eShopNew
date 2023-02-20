using Infrastructure.Services.Interfaces;

namespace Infrastructure.Services
{
    public class JsonSerializer : IJsonSerializer
    {
        public string Serialize<T>(T data)
        {
            return JsonConvert.SerializeObject(data);
        }

        public T Deserialize<T>(string data)
        {
            return JsonConvert.DeserializeObject<T>(data) !;
        }
    }
}
