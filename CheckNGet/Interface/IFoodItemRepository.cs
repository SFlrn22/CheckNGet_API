using CheckNGet.Models;

namespace CheckNGet.Interface
{
    public interface IFoodItemRepository
    {
        ICollection<FoodItem> GetFoodItems();
        FoodItem GetFoodItem(int id);
        FoodItem GetFoodItemByName(string name);
        bool FoodItemExists(int id);  
    }
}
