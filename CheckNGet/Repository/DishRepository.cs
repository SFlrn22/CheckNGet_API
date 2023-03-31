using AutoMapper;
using CheckNGet.Data;
using CheckNGet.Interface;
using CheckNGet.Models;

namespace CheckNGet.Repository
{
    public class DishRepository : IDishRepository
    {
        private readonly DBContext _context;

        public DishRepository(DBContext context)
        {
            _context = context;
        }
        public bool DishExists(int id)
        {
            return _context.Dishes.Any(fi => fi.Id == id);
        }

        public Dish GetDish(int id)
        {
            return _context.Dishes.Where(fi => fi.Id == id).FirstOrDefault();
        }

        public Dish GetDishByName(string name)
        {
            return _context.Dishes.Where(fi => fi.Name == name).FirstOrDefault();
        }

        public ICollection<Dish> GetDishes()
        {
            return _context.Dishes.OrderBy(fi => fi.Id).ToList();
        }
    }
}
