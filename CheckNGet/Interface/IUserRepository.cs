﻿using CheckNGet.Models;
using CheckNGet.Models.DTO;

namespace CheckNGet.Interface
{
    public interface IUserRepository
    {
        ICollection<User> GetUsers();
        User GetUser(int id);
        User GetUser(string username);
        ICollection<Order> GetOrdersByUser(int userId);
        User CompareUsers(UserCreateDTO userCreate);
        bool UserExists(int userId);
        bool UserExists(string username);
        bool CreateUser(User user);
        bool UpdateUser(User user);
        bool DeleteUser(User user);
        bool Save();
    }
}
