namespace Catalog.Host.Models.Request.AddRequests
{
    public class AddAnomalyRequest
    {
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Name { get; set; } = null!;

        [Range(1, int.MaxValue)]
        public int TypeId { get; set; }

        [Range(1, int.MaxValue)]
        public int LocationId { get; set; }

        [Range(1, int.MaxValue)]
        public int FrequencyId { get; set; }
    }
}
