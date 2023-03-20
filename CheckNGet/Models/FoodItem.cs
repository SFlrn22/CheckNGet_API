namespace CheckNGet.Models
{
    public class FoodItem
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImgUrl { get; set; }
        public ICollection<MenuItem> MenuItems { get; set; }
        public ICollection<CategoryItem> CategoryItems { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
