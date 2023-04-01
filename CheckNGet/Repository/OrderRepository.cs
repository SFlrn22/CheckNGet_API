using CheckNGet.Data;
using CheckNGet.Interface;
using CheckNGet.Models;
using Microsoft.EntityFrameworkCore;

namespace CheckNGet.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DBContext _context;

        public OrderRepository(DBContext context)
        {
            _context = context;
        }
        public ICollection<Dish> GetDishesFromOrder(int orderId)
        {
            return _context.OrderDishes.Where(od => od.OrderId == orderId).Select(d => d.Dish).ToList();
        }

        public Order GetOrder(int orderId)
        {
            return _context.Orders.Where(o => o.Id == orderId).FirstOrDefault();
        }

        public ICollection<Order> GetOrders()
        {
            return _context.Orders.OrderBy(o => o.Id).ToList();
        }

        public User GetUserFromOrder(int orderId)
        {
            return _context.Orders.Where(o => o.Id == orderId).Select(u => u.User).FirstOrDefault();
        }

        public bool OrderExists(int orderId)
        {
            return _context.Orders.Any(o => o.Id == orderId);
        }
    }
}
