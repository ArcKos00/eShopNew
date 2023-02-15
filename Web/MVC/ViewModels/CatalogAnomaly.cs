namespace MVC.ViewModels
{
    public class CatalogAnomaly
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public CatalogType Type { get; set; } = null!;
        public CatalogLocation Location { get; set; } = null!;
        public CatalogMeets Meets { get; set; } = null!;
    }
}
