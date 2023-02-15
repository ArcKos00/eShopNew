namespace Catalog.Host.Models.Request.AddRequests
{
    public class AddSimpleTypeRequest
    {
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Name { get; set; } = null!;
    }
}
