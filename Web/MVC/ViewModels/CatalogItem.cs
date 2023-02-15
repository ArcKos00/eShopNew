namespace MVC.ViewModels
{
    public class CatalogItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Cost { get; set; }
        public string Nature { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;

        public CatalogAnomaly Anomaly { get; set; } = null!;
        public CatalogType Type { get; set; } = null!;
        public CatalogMeets Meet { get; set; } = null!;
        public CatalogCharacteristic Values { get; set; } = null!;
    }
}
