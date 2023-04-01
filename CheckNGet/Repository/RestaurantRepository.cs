using AutoMapper;
using CheckNGet.Data;
using CheckNGet.Interface;
using CheckNGet.Models;

namespace CheckNGet.Repository
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly DBContext _context;

        public RestaurantRepository(DBContext context) 
        {
            _context = context;
        }

        public bool CreateRestaurant(Restaurant restaurant)
        {
            _context.Add(restaurant);
            return Save();
        }

        public bool DeleteRestaurant(Restaurant restaurant)
        {
            _context.Remove(restaurant);
            return Save();
        }

        public ICollection<Dish> GetDishByRestaurant(int restaurantId)
        {
            return _context.RestaurantDishes.Where(r => r.RestaurantId == restaurantId).Select(d => d.Dish).ToList();
        }

        public Restaurant GetRestaurant(int id)
        {
            return _context.Restaurants.Where(r => r.Id == id).FirstOrDefault();
        }

        public Restaurant GetRestaurant(string name)
        {
            return _context.Restaurants.Where(r => r.RestaurantName == name).FirstOrDefault();
        }

        public ICollection<Restaurant> GetRestaurants()
        {
            return _context.Restaurants.OrderBy(r => r.Id).ToList();
        }

        public bool RestaurantExists(int restaurantId)
        {
            return _context.Restaurants.Any(r => r.Id == restaurantId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateRestaurant(Restaurant restaurant)
        {
            _context.Update(restaurant);
            return Save();
        }
    }
}
