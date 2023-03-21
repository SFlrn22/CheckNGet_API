namespace CheckNGet.Models
{
    public class FoodItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string ImgUrl { get; set; } = null!;
        public ICollection<MenuItem> MenuItems { get; set; }
        public ICollection<CategoryItem> CategoryItems { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
