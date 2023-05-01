using CheckNGet.Models;
using CheckNGet.Models.DTO;

namespace CheckNGet.Interface
{
    public interface IOrderRepository
    {
        ICollection<Order> GetOrders();
        Order GetOrder(int orderId);
        User GetUserFromOrder(int orderId);
        ICollection<Dish> GetDishesFromOrder(int orderId);
        Order CompareOrders(OrderDTO orderCreate);
        bool OrderExists(int orderId);
        bool CreateOrder(int dishId, Order order);
        bool UpdateOrder(int dishId, Order order);
        bool DeleteOrder(Order order);
        bool Save();
    }
}
