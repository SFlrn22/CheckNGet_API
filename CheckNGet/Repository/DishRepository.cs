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

        public bool CreateDish(int restaurantId, int categoryId, Dish dish)
        {
            var restaurant = _context.Restaurants.Where(r => r.Id == restaurantId).FirstOrDefault();

            var category = _context.Categories.Where(c => c.Id == categoryId).FirstOrDefault();

            var restaurantDish = new RestaurantDish
            {
                Restaurant = restaurant,
                Dish = dish
            };

            var categoryDish = new CategoryDish
            {
                Category = category,
                Dish = dish
            };

            _context.Add(restaurantDish);
            _context.Add(categoryDish);
            _context.Add(dish);

            return Save();
        }

        public bool DeleteDish(Dish dish)
        {
            _context.Remove(dish);
            return Save();
        }

        public bool DeleteDishes(List<Dish> dishes)
        {
            _context.RemoveRange(dishes);
            return Save();
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

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateDish(int restaurantId, int categoryId, Dish dish)
        {
            _context.Update(dish);
            return Save();
        }
    }
}
