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
    }
}
