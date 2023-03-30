using CheckNGet.Models;

namespace CheckNGet.Interface
{
    public interface ICategoryRepository
    {
        ICollection<Category> GetCategories();
        Category GetCategory(int id);
        ICollection<FoodItem> GetItemByCategory(int categoriaId);
        bool CategoryExists(int id);
    }
}
