using Coffee_Shop.Models;
using CoffeeShop.Data.Models;
using CoffeShop.Data.Models;
using CoffeShop.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        #region Person Methods
        public IEnumerable<Person> GetPersonList()
        {
            return db.Persons;
        }
        public Person? GetPerson(int id)
        {
            return db.Persons.Find(id);
        }
        public void CreatePerson(Person person)
        {
            db.Persons.Add(person);
        }

        public void UpdatePerson(Person person)
        {
            db.Entry(person).State = EntityState.Modified;
        }

        public void DeletePerson(int id)
        {
            Person? person = db.Persons.Find(id);

            if (person != null)
            {
                db.Persons.Remove(person);
            }
        }

        #endregion

        #region Product Methods

        public IEnumerable<Product> GetProductList()
        {
            return db.Products;
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