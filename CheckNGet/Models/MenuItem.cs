namespace CheckNGet.Models
{
    public class MenuItem
    {
        public int MenuId { get; set; }
        public int FoodItemId { get; set; }
        public Menu Menu { get; set; }
        public FoodItem FoodItem { get; set; }
    }
}
