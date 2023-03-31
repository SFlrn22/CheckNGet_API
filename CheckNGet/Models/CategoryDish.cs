namespace CheckNGet.Models
{
    public class CategoryDish
    {
        public int CategoryId { get; set; } 
        public int DishId { get; set; }
        public Category Category { get; set; }
        public Dish Dish { get; set; }
    }
}
