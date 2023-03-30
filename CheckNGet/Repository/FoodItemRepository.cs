using AutoMapper;
using CheckNGet.Data;
using CheckNGet.Interface;
using CheckNGet.Models;

namespace CheckNGet.Repository
{
    public class FoodItemRepository : IFoodItemRepository
    {
        private readonly DBContext _context;

        public FoodItemRepository(DBContext context)
        {
            _context = context;
        }
        public bool FoodItemExists(int id)
        {
            return _context.FoodItems.Any(fi => fi.Id == id);
        }

        public FoodItem GetFoodItem(int id)
        {
            return _context.FoodItems.Where(fi => fi.Id == id).FirstOrDefault();
        }

        public FoodItem GetFoodItemByName(string name)
        {
            return _context.FoodItems.Where(fi => fi.Name == name).FirstOrDefault();
        }

        public ICollection<FoodItem> GetFoodItems()
        {
            return _context.FoodItems.OrderBy(fi => fi.Id).ToList();
        }
    }
}
