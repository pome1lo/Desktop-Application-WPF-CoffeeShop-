using Coffee_Shop.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Coffee_Shop.Database.Repositories
{
    internal class AddressRepositoryPostgreSQL : IRepository<Address>
    {
        private ApplicationContext db { get; set; }

        #region Constructor 

        public AddressRepositoryPostgreSQL()
        {
            this.db = ApplicationContext.GetContext();
        }

        #endregion

        #region Methods

        public void Create(Address item)
        {
            db.Addresses.Add(item);
        }

        public void Delete(int id)
        {
            Address? address = db.Addresses.Find(id);

            if (address != null)
            {
                db.Addresses.Remove(address);
            }
        }

        public Address? Get(int id)
        {
            return db.Addresses.Find(id);
        }

        public IEnumerable<Address> GetIEnumerable()
        {
            return db.Addresses;
        }

        public void Update(Address item)
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
