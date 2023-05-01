using CheckNGet.Data;
using CheckNGet.Interface;
using CheckNGet.Models;
using CheckNGet.Models.DTO;

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

        public bool CreateCategory(Category category)
        {
            _context.Add(category);
            return Save();
        }

        public bool DeleteCategory(Category category)
        {
            _context.Remove(category);
            return Save();
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

        public Category CompareCategories(CategoryDTO categoryCreate)
        {
            return GetCategories()
                .Where(c => c.Name.Trim().ToUpper() == categoryCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateCategory(Category category)
        {
            _context.Update(category);
            return Save();
        }
    }
}
