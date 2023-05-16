using Coffee_Shop.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Coffee_Shop.Database.Repositories
{
    internal class OrderRepositoryPostgreSQL : IRepository<Order>
    {
        private ApplicationContext db { get; set; }

        #region Constructor 

        public OrderRepositoryPostgreSQL()
        {
            this.db = ApplicationContext.GetContext();
        }

        #endregion

        #region Methods

        public void Create(Order item)
        {
            item.Id = db.Orders.Count() + 1;
            db.Orders.Add(item);
        }

        public void Delete(int id)
        {
            Order? order = db.Orders.Find(id);

            if (order != null)
            {
                db.Orders.Remove(order);
            }
        }

        public Order? Get(int id)
        {
            return GetIEnumerable().ToList().Find(x => x.Id == id);
        }

        public IEnumerable<Order> GetIEnumerable()
        {
            return db.Orders.Include(x => x.User).Include(x => x.Status);
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Update(Order item)
        {
            db.Entry(item).State = EntityState.Modified;
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
