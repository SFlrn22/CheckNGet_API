using CheckNGet.Models;

namespace CheckNGet.Interface
{
    public interface IUserRepository
    {
        ICollection<User> GetUsers();
        User GetUser(int id);
        User GetUser(string username);
        ICollection<Order> GetOrdersByUser(int userId);
        bool UserExists(int userId);
        bool UserExists(string username);
    }
}
