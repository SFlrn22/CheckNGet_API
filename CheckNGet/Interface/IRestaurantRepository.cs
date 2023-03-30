using CheckNGet.Models;

namespace CheckNGet.Interface
{
    public interface IRestaurantRepository
    {
        ICollection<Restaurant> GetRestaurants();
        Restaurant GetRestaurant(int id);
        Restaurant GetRestaurant(string name);
        Restaurant GetRestaurantContactNumber(string contactNumber);
        Restaurant GetRestaurantAddress(string address);
        Restaurant GetRestaurantContactAddress(string email);
        Restaurant GetRestaurantImgUrl(string url);
        bool RestaurantExists(int restaurantId);
    }
}
