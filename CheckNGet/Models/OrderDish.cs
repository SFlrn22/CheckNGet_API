namespace CheckNGet.Models
{
    public class OrderDish
    {
        public int OrderId { get; set; }
        public int DishId { get; set; }
        public Order Order { get; set; }
        public Dish Dish { get; set; }
    }
}
