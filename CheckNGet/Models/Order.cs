namespace CheckNGet.Models
{
    public class Order
    {
        public int ID { get; set; }
        public string OrderCode { get; set; }
        public DateTime OrderDate { get; set; }
        public User User { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
