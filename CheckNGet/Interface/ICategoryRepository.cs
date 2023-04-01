using CheckNGet.Models;

namespace CheckNGet.Interface
{
    public interface ICategoryRepository
    {
        ICollection<Category> GetCategories();
        Category GetCategory(int id);
        ICollection<Dish> GetDishByCategory(int categoryId);
        bool CategoryExists(int id);
        bool CreateCategory(Category category);
        bool Save();
    }
}
