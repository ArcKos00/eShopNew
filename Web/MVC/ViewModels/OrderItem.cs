namespace MVC.ViewModels
{
    public class OrderItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Cost { get; set; }
    }
}
