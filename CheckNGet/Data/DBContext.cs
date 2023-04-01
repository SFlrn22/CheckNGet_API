using CheckNGet.Models;
using Microsoft.EntityFrameworkCore;

namespace CheckNGet.Data
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<OrderDish> OrderDishes{ get; set; }
        public DbSet<CategoryDish> CategoryDishes { get; set; }
        public DbSet<RestaurantDish> RestaurantDishes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoryDish>()
                .HasKey(cd => new { cd.CategoryId, cd.DishId } );
            modelBuilder.Entity<CategoryDish>()
                .HasOne(c => c.Category)
                .WithMany(cd => cd.CategoryDishes)
                .HasForeignKey(c => c.CategoryId);
            modelBuilder.Entity<CategoryDish>()
                .HasOne(d => d.Dish)
                .WithMany(cd => cd.CategoryDishes)
                .HasForeignKey(d => d.DishId);

            modelBuilder.Entity<OrderDish>()
                .HasKey(od => new { od.OrderId, od.DishId } );
            modelBuilder.Entity<OrderDish>()
                .HasOne(o => o.Order)
                .WithMany(od => od.OrderDishes)
                .HasForeignKey(o => o.OrderId);
            modelBuilder.Entity<OrderDish>()
                .HasOne(d => d.Dish)
                .WithMany(od => od.OrderDishes)
                .HasForeignKey(d => d.DishId);

            modelBuilder.Entity<RestaurantDish>()
                .HasKey(rd => new { rd.RestaurantId, rd.DishId });
            modelBuilder.Entity<RestaurantDish>()
                .HasOne(r => r.Restaurant)
                .WithMany(rd => rd.RestaurantDishes)
                .HasForeignKey(r => r.RestaurantId);
            modelBuilder.Entity<RestaurantDish>()
                .HasOne(d => d.Dish)
                .WithMany(rd => rd.RestaurantDishes)
                .HasForeignKey(d => d.DishId);
        }
    }
}
