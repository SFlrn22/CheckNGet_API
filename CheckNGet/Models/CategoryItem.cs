namespace CheckNGet.Models
{
    public class CategoryItem
    {
        public int CategoryId { get; set; } 
        public int FoodItemId { get; set; }
        public Category Category { get; set; }
        public FoodItem FoodItem { get; set; }
    }
}
