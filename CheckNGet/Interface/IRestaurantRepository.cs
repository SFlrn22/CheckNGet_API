using CheckNGet.Models;

namespace CheckNGet.Interface
{
    public interface IRestaurantRepository
    {
        ICollection<Restaurant> GetRestaurants();
        Restaurant GetRestaurant(int id);
        Restaurant GetRestaurant(string name);
        ICollection<Dish> GetDishByRestaurant(int restaurantId);
        bool RestaurantExists(int restaurantId);
        bool CreateRestaurant(Restaurant restaurant);
        bool UpdateRestaurant(Restaurant restaurant);
        bool DeleteRestaurant(Restaurant restaurant);
        bool Save();
    }
}
