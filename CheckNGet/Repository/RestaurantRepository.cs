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

        public Restaurant GetRestaurantAddress(string address)
        {
            return _context.Restaurants.Where(r => r.RestaurantAddress == address).FirstOrDefault();
        }

        public Restaurant GetRestaurantContactNumber(string contactNumber)
        {
            return _context.Restaurants.Where(r => r.ContactNumber == contactNumber).FirstOrDefault();
        }

        public Restaurant GetRestaurantContactAddress(string email)
        {
            return _context.Restaurants.Where(r => r.ContactAddress == email).FirstOrDefault();
        }

        public Restaurant GetRestaurantImgUrl(string url)
        {
            return _context.Restaurants.Where(r => r.ImgUrl == url).FirstOrDefault();
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
