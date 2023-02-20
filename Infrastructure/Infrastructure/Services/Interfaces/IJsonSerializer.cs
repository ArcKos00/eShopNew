namespace Infrastructure.Services.Interfaces
{
    public interface IJsonSerializer
    {
        public string Serialize<T>(T data);
        public T Deserialize<T>(string data);
    }
}
