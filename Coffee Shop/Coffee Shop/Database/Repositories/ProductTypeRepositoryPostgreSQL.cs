using Coffee_Shop.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Coffee_Shop.Database.Repositories
{
    internal class ProductTypeRepositoryPostgreSQL : IRepository<ProductType>
    {
        private ApplicationContext db { get; set; }

        #region Constructor 

        public ProductTypeRepositoryPostgreSQL()
        {
            this.db = ApplicationContext.GetContext();
        }

        #endregion

        #region Methods

        public void Create(ProductType item)
        {
            db.ProductTypes.Add(item);
        }

        public void Delete(int id)
        {
            ProductType? type = db.ProductTypes.Find(id);

            if (type != null)
            {
                db.ProductTypes.Remove(type);
            }
        }

        public ProductType GetByName(string Name)
        {
            if (!db.ProductTypes.ToList().Any(x => x.Name == Name))
            {
                db.ProductTypes.Add(new ProductType() { Name = Name });
                db.SaveChanges();
            }
            return db.ProductTypes.First(x => x.Name == Name);
        }

        public ProductType? Get(int id)
        {
            return db.ProductTypes.Find(id);
        }

        public IEnumerable<ProductType> GetIEnumerable()
        {
            return db.ProductTypes;
        }

        public void Update(ProductType item)
        {
            db.Entry(item).State = EntityState.Modified;
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
