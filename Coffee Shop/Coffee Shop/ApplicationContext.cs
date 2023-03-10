using CoffeeShop.Data.Models;
using CoffeShop.Data.Models;
using CoffeShop.Models;
using Microsoft.EntityFrameworkCore;

namespace CoffeShop.Data
{
    class ApplicationContext : DbContext
    {
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Description> Descriptions { get; set; } = null!;
        public DbSet<Person> Persons { get; set; } = null!;


        private static ApplicationContext? Instance;

        public static ApplicationContext GetContext()
        {
            if (Instance == null)
            {
                Instance = new ApplicationContext();
            }
            return Instance;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=CoffeeShopDB;Username=postgres;Password=@2o!&69");
        }
    }
}
