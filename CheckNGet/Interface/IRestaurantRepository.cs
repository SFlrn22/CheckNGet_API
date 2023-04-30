using CheckNGet.Models;
using CheckNGet.Models.DTO;

namespace CheckNGet.Interface
{
    public interface IRestaurantRepository
    {
        ICollection<Restaurant> GetRestaurants();
        Restaurant GetRestaurant(int id);
        Restaurant GetRestaurant(string name);
        ICollection<Dish> GetDishByRestaurant(int restaurantId);
        Restaurant CompareRestaurants(RestaurantDTO restaurantCreate);
        bool RestaurantExists(int restaurantId);
        bool CreateRestaurant(Restaurant restaurant);
        bool UpdateRestaurant(Restaurant restaurant);
        bool DeleteRestaurant(Restaurant restaurant);
        bool Save();
    }
}
