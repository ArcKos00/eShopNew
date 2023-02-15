namespace Catalog.Host.Models.Request.AddRequests
{
    public class AddArtefactRequest
    {
        [MinLength(3)]
        [MaxLength(50)]
        public string Name { get; set; } = null!;

        [MinLength(3)]
        [MaxLength(50)]
        public string Nature { get; set; } = null!;

        [MinLength(3)]
        [MaxLength(50)]
        public string ImagePath { get; set; } = null!;

        [Range(0, double.MaxValue)]
        public decimal Cost { get; set; }

        [Range(1, int.MaxValue)]
        public int CharacteristicId { get; set; }

        [Range(1, int.MaxValue)]
        public int AnomalyId { get; set; }

        [Range(1, int.MaxValue)]
        public int TypeId { get; set; }

        [Range(1, int.MaxValue)]
        public int FrequencyId { get; set; }
    }
}
