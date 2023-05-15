using CoffeeShop.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee_Shop.Database.Repositories
{
    class UserRepositoryPosgreSQL : IRepository<User>
    {
        private ApplicationContext db { get; set; }

        #region Constructor 

        public UserRepositoryPosgreSQL()
        {
            this.db = ApplicationContext.GetContext();
        }

        #endregion


        #region User Methods

        public IEnumerable<User> GetIEnumerable()
        {
            return db.Users.Include(x => x.BankCard)
                .Include(x => x.ProductsFromBasket)
                .Include(x => x.SocialNetworks)
                .Include(x => x.Notifications);
        }
        public User? Get(int id)
        {
            return GetIEnumerable().ToList().Find(x => x.Id == id);
        }
        public void Create(User user)
        {
            user.Id = db.Users.Count() + 1;
            db.Users.Add(user);
        }

        public void Update(User user)
        {
            db.Entry(user).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            User? user = db.Users.Find(id);

            if (user != null)
            {
                db.Users.Remove(user);
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
