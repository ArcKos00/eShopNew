namespace Catalog.Host.Models.Dtos
{
    public class Artefact
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Cost { get; set; }
        public string Nature { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;

        public Characteristic Values { get; set; } = null!;
        public Anomaly Anomaly { get; set; } = null!;
        public AbnormalType Type { get; set; } = null!;
        public Frequency Meets { get; set; } = null!;
    }
}
