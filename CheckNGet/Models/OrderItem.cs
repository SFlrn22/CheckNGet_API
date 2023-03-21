namespace CheckNGet.Models
{
    public class OrderItem
    {
        public int OrderId { get; set; }
        public int FoodItemId { get; set; }
        public Order Order { get; set; }
        public FoodItem FoodItem { get; set; }
    }
}
