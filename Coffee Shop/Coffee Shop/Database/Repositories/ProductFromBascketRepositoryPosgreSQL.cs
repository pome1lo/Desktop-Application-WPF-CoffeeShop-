using Coffee_Shop.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee_Shop.Database.Repositories
{
    class ProductFromBascketRepositoryPosgreSQL : IRepository<ProductFromBasket>
    {
        private ApplicationContext db { get; set; }

        #region Constructor 

        public ProductFromBascketRepositoryPosgreSQL()
        {
            this.db = ApplicationContext.GetContext();
        }

        #endregion

        #region Methods

        public IEnumerable<ProductFromBasket> GetIEnumerable()
        {
            return db.ProductsFromBasket.Include(x => x.Product);
        }

        public ProductFromBasket? Get(int product)
        {
            return db.ProductsFromBasket.Find(product);
        }

        public void Create(ProductFromBasket item)
        {
            db.ProductsFromBasket.Add(item);
        }

        public void Update(ProductFromBasket item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            ProductFromBasket? productFromBasket = db.ProductsFromBasket.Find(id);

            if (productFromBasket != null)
            {
                db.ProductsFromBasket.Remove(productFromBasket);
            }
        }

        public void Save()
        {
            db.SaveChanges();
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

        #endregion
    }
}
