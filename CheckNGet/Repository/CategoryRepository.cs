using CheckNGet.Data;
using CheckNGet.Interface;
using CheckNGet.Models;

namespace CheckNGet.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DBContext _context;

        public CategoryRepository(DBContext context)
        {
            _context = context;
        }
        public bool CategoryExists(int id)
        {
            return _context.Categories.Any(c => c.Id == id);
        }

        public ICollection<Category> GetCategories()
        {
            return _context.Categories.OrderBy(c => c.Id).ToList();
        }

        public Category GetCategory(int id)
        {
            return _context.Categories.Where(c => c.Id == id).FirstOrDefault();
        }

        public ICollection<Dish> GetDishByCategory(int categoryId)
        {
            return _context.CategoryDishes.Where(c => c.CategoryId == categoryId).Select(i => i.Dish).ToList();
        }
    }
}
