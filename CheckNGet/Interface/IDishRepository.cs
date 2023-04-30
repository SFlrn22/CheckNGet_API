using CheckNGet.Models;
using CheckNGet.Models.DTO;

namespace CheckNGet.Interface
{
    public interface IDishRepository
    {
        ICollection<Dish> GetDishes();
        Dish GetDish(int id);
        Dish GetDishByName(string name);
        Dish CompareDishes(DishDTO dishCreate);
        bool DishExists(int id);  
        bool CreateDish(int restaurantId, int categoryId, Dish dish);
        bool UpdateDish(int restaurantId, int categoryId, Dish dish);
        bool DeleteDish(Dish dish);
        bool DeleteDishes(List<Dish> dishes);
        bool Save();
    }
}
