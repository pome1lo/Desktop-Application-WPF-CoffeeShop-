using Coffee_Shop.Models;
using Coffee_Shop.Models.Entities;
using CoffeeShop.Data.Models;
using CoffeShop.Data.Models;
using CoffeShop.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Coffee_Shop.Database
{
    internal class PostgreSQLRepository : IRepository
    {
        private ApplicationContext db { get; set; }

        public PostgreSQLRepository()
        {
            this.db = new ApplicationContext();
        }
        public void Save()
        {
            db.SaveChanges();
        }

        #region User Methods

        public IEnumerable<User> GetUserList()
        {
            return db.Users.Include(x => x.BankCard).Include(x => x.ProductsFromBasket).Include(x => x.SocialNetworks);
        }
        public User? GetUser(int id)
        {
            return GetUserList().ToList().Find(x => x.Id == id);
        }
        public void CreateUser(User user)
        {
            user.Id = db.Users.Count() + 1;
            db.Users.Add(user);
        }

        public void UpdateUser(User user)
        {
            db.Entry(user).State = EntityState.Modified;
        }

        public void DeleteUser(int id)
        {
            User? user = db.Users.Find(id);

            if (user != null)
            {
                db.Users.Remove(user);
            }
        }

        #endregion

        #region Product Methods

        public IEnumerable<Product> GetProductList()
        {
            //IEnumerable<Product> products = db.Products;

            //products.ToList().ForEach(x => x.Description = db.Descriptions.ToArray()[x.Id - 1]);

            return db.Products.Include(x => x.ProductType).Include(x => x.Description);
        }

        public void UpdateProduct(Product product)
        {
            db.Entry(product).State = EntityState.Modified;
        }

        public Product? GetProduct(int id)
        {
            return db.Products.Find(id);
        }

        public void CreateProduct(Product product)
        {
            product.Id = db.Products.Count() + 1;
            db.Products.Add(product);
        }

        public void DeleteProduct(int id)
        {
            Product? product = db.Products.Find(id);

            if (product != null)
            {
                db.Products.Remove(product);
            }
        }

        #endregion

        #region News Methods

        public IEnumerable<News> GetNewsList()
        {
            return db.News;
        }

        public News? GetNews(int id)
        {
            return db.News.Find(id);
        }

        public void CreateNews(News item)
        {
            db.News.Add(item);
        }

        public void UpdateNews(News item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public void DeleteNews(int id)
        {
            News? news = db.News.Find(id);

            if (news != null)
            {
                db.News.Remove(news);
            }
        }

        #endregion

        #region Description Methods
        public IEnumerable<Description> GetDescriptionList()
        {
            return db.Descriptions;
        }
        public Description? GetDescription(int id)
        {
            return db.Descriptions.Find(id);
        }

        #endregion

        #region Product From Basket

        public IEnumerable<ProductFromBasket> GetProductFromBasketList()
        {
            return db.ProductsFromBasket.Include(x => x.Product);
        }

        public ProductFromBasket? GetProductFromBasket(int product)
        {           
            return db.ProductsFromBasket.Find(product);
        }

        public void CreateProductFromBasket(ProductFromBasket item)
        {
            db.ProductsFromBasket.Add(item);
        }

        public void UpdateProductFromBasket(ProductFromBasket item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public void DeleteProductFromBasket(int id)
        {
            ProductFromBasket? productFromBasket = db.ProductsFromBasket.Find(id);

            if (productFromBasket != null)
            {
                db.ProductsFromBasket.Remove(productFromBasket);
            }
        }

        #endregion

        #region Bank Card
        
        public IEnumerable<BankCard> GetBankCardList()
        {
            return db.BankCards;
        }

        public BankCard? GetBankCard(int id)
        {
            return db.BankCards.Find(id);
        }

        public void CreateBankCard(BankCard item)
        {
            db.BankCards.Add(item);
        }

        public void UpdateBankCard(BankCard item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public void DeleteBankCard(int id)
        {
            BankCard? bankCard = db.BankCards.Find(id);

            if (bankCard != null)
            {
                db.BankCards.Remove(bankCard);
            }
        }

        #endregion

        public ProductType GetProductType(string Name)
        {
            if (!db.ProductTypes.ToList().Any(x => x.Name == Name))
            {
                db.ProductTypes.Add(new ProductType() { Name = Name });
                db.SaveChanges();
            }
            return db.ProductTypes.First(x => x.Name == Name);
        }

        #region Dispose

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}