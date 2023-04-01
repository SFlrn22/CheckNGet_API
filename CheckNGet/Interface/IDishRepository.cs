using CheckNGet.Models;

namespace CheckNGet.Interface
{
    public interface IDishRepository
    {
        ICollection<Dish> GetDishes();
        Dish GetDish(int id);
        Dish GetDishByName(string name);
        bool DishExists(int id);  
        bool CreateDish(int restaurantId, int categoryId, Dish dish);
        bool UpdateDish(int restaurantId, int categoryId, Dish dish);
        bool Save();
    }
}
