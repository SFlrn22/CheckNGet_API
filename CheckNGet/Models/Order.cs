namespace CheckNGet.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string OrderCode { get; set; } = null!;
        public DateTime OrderDate { get; set; }
        public User User { get; set; }
        public ICollection<OrderDish> OrderDishes{ get; set; }
    }
}
