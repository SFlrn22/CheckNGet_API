namespace CheckNGet.Models
{
    public class OrderItem
    {
        public int OrderID { get; set; }
        public int FoodItemID { get; set; }
        public Order Order { get; set; }
        public FoodItem FoodItem { get; set; }
    }
}
