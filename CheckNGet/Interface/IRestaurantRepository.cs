using CheckNGet.Models;

namespace CheckNGet.Interface
{
    public interface IRestaurantRepository
    {
        ICollection<Restaurant> GetRestaurants();
        Restaurant GetRestaurant(int id);
        Restaurant GetRestaurant(string name);
        bool RestaurantExists(int restaurantId);
        bool CreateRestaurant(Restaurant restaurant);
        bool Save();
    }
}
