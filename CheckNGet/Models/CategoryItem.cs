namespace CheckNGet.Models
{
    public class CategoryItem
    {
        public int CategoryID { get; set; } 
        public int FoodItemID { get; set; }
        public Category Category { get; set; }
        public FoodItem FoodItem { get; set; }
    }
}
