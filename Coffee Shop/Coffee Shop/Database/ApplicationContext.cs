using Coffee_Shop.Models;
using Coffee_Shop.Models.Entities;
using CoffeeShop.Data.Models;
using CoffeShop.Data.Models;
using CoffeShop.Models;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace Coffee_Shop.Database
{
    internal class ApplicationContext : DbContext
    {
        public DbSet<BankCard> BankCards { get; set; } = null!;
        public DbSet<ProductFromBasket> ProductsFromBasket { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<News> News { get; set; } = null!;
        public DbSet<Notification> Notifications { get; set; } = null!;
        public DbSet<Description> Descriptions { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<SocialNetworks> SocialNetworks { get; set; } = null!;
        public DbSet<ProductType> ProductTypes { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<OrderStatus> OrderStatuses{ get; set; } = null!;
        public DbSet<Address> Addresses { get; set; } = null!;


        private static string ConnectionString { get; set; } = string.Empty;
        private static ApplicationContext? Instance;
        public ApplicationContext() 
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionStringPostgressSQL"].ConnectionString;
        }
        
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
            optionsBuilder.UseNpgsql(ConnectionString);
            //optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=COFFEE_SHOP;Username=postgres;Password=@2o!&69");
        }
    }
}