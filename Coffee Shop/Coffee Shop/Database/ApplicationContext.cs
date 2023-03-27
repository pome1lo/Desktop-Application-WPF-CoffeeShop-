using Coffee_Shop.Models;
using Coffee_Shop.Models.Entities;
using CoffeeShop.Data.Models;
using CoffeShop.Data.Models;
using CoffeShop.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Coffee_Shop.Database
{
    internal class ApplicationContext : DbContext
    {
        public DbSet<ProductFromBasket> ProductFromBasket { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Description> Descriptions { get; set; }
        public DbSet<Person> Persons { get; set; }

        private static string ConnectionString { get; set; } = string.Empty;
        private static IRepository? database { get; set; }

        private enum ChoiceDataBase : byte
        {
            ServerSQL = 0,
            PostgreSQL = 1,
        }

        private static ApplicationContext? Instance;

        public static ApplicationContext GetContext()
        {
            if (Instance == null)
            {
                Instance = new ApplicationContext();
            }
            return Instance;
        }

        public static void SetConnectionString(byte choice)
        {
            switch (choice)
            {
                case (byte)ChoiceDataBase.ServerSQL:
                    {
                        ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionStringServerSQL"].ConnectionString;
                        database = new SQLServerRepository();
                        break;
                    }
                case (byte)ChoiceDataBase.PostgreSQL:
                    {
                        ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionStringPostgressSQL"].ConnectionString;
                        database = new PostgreSQLRepository();
                        break;
                    }
            }
        }

        public static IRepository? GetTheCurrentDatabase()
        {
            return database;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(ConnectionString);
        }
    }
}