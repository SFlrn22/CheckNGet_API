using CheckNGet.Data;
using CheckNGet.Interface;
using CheckNGet.Models;
using CheckNGet.Models.DTO;
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

        public bool CreateOrder(int dishId, Order order)
        {
            var dish = _context.Dishes.Where(d => d.Id == dishId).FirstOrDefault();

            var orderDish = new OrderDish()
            {
                Order = order,
                Dish = dish
            };

            _context.Add(orderDish);

            _context.Add(order);

            return Save();
        }

        public bool DeleteOrder(Order order)
        {
            _context.Remove(order);
            return Save();
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

        public Order CompareOrders(OrderDTO orderCreate)
        {
            return GetOrders()
                .Where(o => o.OrderCode.Trim().ToUpper() == orderCreate.OrderCode.TrimEnd().ToUpper())
                .FirstOrDefault();
        }

        public bool OrderExists(int orderId)
        {
            return _context.Orders.Any(o => o.Id == orderId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateOrder(int dishId, Order order)
        {
            _context.Update(order);
            return Save();
        }
    }
}
