using Microsoft.EntityFrameworkCore;
using System;

namespace CheckNGet.Models
{
    [Index(nameof(OrderCode), IsUnique = true)]
    public class Order
    {
        public int Id { get; set; }
        public string OrderCode { get; set; } = null!;
        public DateTime OrderDate { get; set; }
        public User User { get; set; }
        public ICollection<OrderDish> OrderDishes{ get; set; }
    }
}
