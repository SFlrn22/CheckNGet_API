namespace CheckNGet.Models
{
    public class Dish
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string ImgUrl { get; set; } = null!;
        public ICollection<CategoryDish> CategoryDishes { get; set; }
        public ICollection<OrderDish> OrderDishes { get; set; }
        public ICollection<RestaurantDish> RestaurantDishes { get; set; }
    }
}
