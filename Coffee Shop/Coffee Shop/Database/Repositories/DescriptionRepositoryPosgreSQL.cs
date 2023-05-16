using CoffeShop.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Coffee_Shop.Database.Repositories
{
    class DescriptionRepositoryPosgreSQL : IRepository<Description>
    {
        private ApplicationContext db { get; set; }

        #region Constructor 

        public DescriptionRepositoryPosgreSQL()
        {
            this.db = ApplicationContext.GetContext();
        }

        #endregion

        #region Methods

        public IEnumerable<Description> GetIEnumerable()
        {
            return db.Descriptions;
        }
        public Description? Get(int id)
        {
            return db.Descriptions.Find(id);
        }
        public void Delete(int id)
        {
            Description? descriptions = db.Descriptions.Find(id);

            if (descriptions != null)
            {
                db.Descriptions.Remove(descriptions);
            }
        }
        public void Create(Description item)
        {
            db.Descriptions.Add(item);
        }

        public void Update(Description item)
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
