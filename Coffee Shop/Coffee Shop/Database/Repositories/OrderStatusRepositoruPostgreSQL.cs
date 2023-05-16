using Coffee_Shop.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Coffee_Shop.Database.Repositories
{
    internal class OrderStatusRepositoruPostgreSQL : IRepository<OrderStatus>
    {
        private ApplicationContext db { get; set; }

        #region Constructor 

        public OrderStatusRepositoruPostgreSQL()
        {
            this.db = ApplicationContext.GetContext();
        }

        #endregion

        #region Methods

        public void Create(OrderStatus item)
        {
            item.Id = db.OrderStatuses.Count() + 1;
            db.OrderStatuses.Add(item);
        }

        public void Delete(int id)
        {
            OrderStatus? order = db.OrderStatuses.Find(id);

            if (order != null)
            {
                db.OrderStatuses.Remove(order);
            }
        }
        public OrderStatus GetByName(string Name)
        {
            if (!db.OrderStatuses.ToList().Any(x => x.StatusName == Name))
            {
                db.OrderStatuses.Add(new OrderStatus() { StatusName = Name });
                db.SaveChanges();
            }
            return db.OrderStatuses.First(x => x.StatusName == Name);
        }

        public OrderStatus? Get(int id)
        {
            return GetIEnumerable().ToList().Find(x => x.Id == id);
        }

        public IEnumerable<OrderStatus> GetIEnumerable()
        {
            return db.OrderStatuses;
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Update(OrderStatus item)
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
