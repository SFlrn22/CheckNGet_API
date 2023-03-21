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
        public DbSet<Menu> Menus { get; set; }
        public DbSet<FoodItem> FoodItems { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<CategoryItem> CategoryItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MenuItem>()
                .HasKey(mi => new { mi.MenuId, mi.FoodItemId });
            modelBuilder.Entity<MenuItem>()
                .HasOne(m => m.Menu)
                .WithMany(mi => mi.MenuItems)
                .HasForeignKey(m => m.MenuId);
            modelBuilder.Entity<MenuItem>()
                .HasOne(f => f.FoodItem)
                .WithMany(mi => mi.MenuItems)
                .HasForeignKey(f => f.FoodItemId);

            modelBuilder.Entity<CategoryItem>()
                .HasKey(ci => new { ci.CategoryId, ci.FoodItemId });
            modelBuilder.Entity<CategoryItem>()
                .HasOne(c => c.Category)
                .WithMany(ci => ci.CategoryItems)
                .HasForeignKey(c => c.CategoryId);
            modelBuilder.Entity<CategoryItem>()
                .HasOne(f => f.FoodItem)
                .WithMany(ci => ci.CategoryItems)
                .HasForeignKey(f => f.FoodItemId);

            modelBuilder.Entity<OrderItem>()
                .HasKey(oi => new { oi.OrderId, oi.FoodItemId });
            modelBuilder.Entity<OrderItem>()
                .HasOne(o => o.Order)
                .WithMany(oi => oi.OrderItems)
                .HasForeignKey(o => o.OrderId);
            modelBuilder.Entity<OrderItem>()
                .HasOne(f => f.FoodItem)
                .WithMany(oi => oi.OrderItems)
                .HasForeignKey(f => f.FoodItemId);
        }
    }
}
