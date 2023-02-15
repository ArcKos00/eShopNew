namespace Catalog.Host.Data.Entities
{
    public class ArtefactEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Cost { get; set; }
        public string Nature { get; set; } = null!;
        public string ImagePath { get; set; } = null!;

        public int AnomalyId { get; set; }
        public AnomalyEntity Anomaly { get; set; } = null!;
        public int AbnormalTypeId { get; set; }
        public AbnormalTypeEntity Type { get; set; } = null!;
        public int FrequencyId { get; set; }
        public FrequencyEntity Meets { get; set; } = null!;
        public int CharacteristicId { get; set; }
        public CharacteristicEntity Values { get; set; } = null!;
    }
}
