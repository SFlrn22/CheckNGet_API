using CheckNGet.Models;
using CheckNGet.Models.DTO;

namespace CheckNGet.Interface
{
    public interface ICategoryRepository
    {
        ICollection<Category> GetCategories();
        Category GetCategory(int id);
        ICollection<Dish> GetDishByCategory(int categoryId);
        Category CompareCategories(CategoryDTO categoryCreate);
        bool CategoryExists(int id);
        bool CreateCategory(Category category);
        bool UpdateCategory(Category category);
        bool DeleteCategory(Category category);
        bool Save();
    }
}
