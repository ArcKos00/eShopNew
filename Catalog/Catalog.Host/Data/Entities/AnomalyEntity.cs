namespace Catalog.Host.Data.Entities
{
    public class AnomalyEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public int AbnormalTypeId { get; set; }
        public AbnormalTypeEntity Type { get; set; } = null!;
        public int LocationId { get; set; }
        public LocationEntity LocationPlace { get; set; } = null!;
        public int FrequencyId { get; set; }
        public FrequencyEntity Meets { get; set; } = null!;
    }
}
