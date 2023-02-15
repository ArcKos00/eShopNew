namespace Catalog.Host.Models.Dtos
{
    public class Anomaly
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public AbnormalType Type { get; set; } = null!;
        public Location LocationPlace { get; set; } = null!;
        public Frequency Meets { get; set; } = null!;
    }
}
