namespace CheckNGet.Models
{
    public class MenuItem
    {
        public int MenuID { get; set; }
        public int FoodItemID { get; set; }
        public Menu Menu { get; set; }
        public FoodItem FoodItem { get; set; }
    }
}
