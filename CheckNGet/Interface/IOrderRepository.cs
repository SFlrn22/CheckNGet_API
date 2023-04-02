using CheckNGet.Models;

namespace CheckNGet.Interface
{
    public interface IOrderRepository
    {
        ICollection<Order> GetOrders();
        Order GetOrder(int orderId);
        User GetUserFromOrder(int orderId);
        ICollection<Dish> GetDishesFromOrder(int orderId);
        bool OrderExists(int orderId);
        bool CreateOrder(int dishId, Order order);
        bool UpdateOrder(int dishId, Order order);
        bool DeleteOrder(Order order);
        bool Save();
    }
}
