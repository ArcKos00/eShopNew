namespace Catalog.Host.Models.Request.UpdateRequest
{
    public class UpdateStringRequest : BaseRequest
    {
        [Required]
        [MinLength(1)]
        [MaxLength(50)]
        public string UpdateValue { get; set; } = null!;
    }
}
