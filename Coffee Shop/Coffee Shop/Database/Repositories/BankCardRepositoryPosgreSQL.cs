using Coffee_Shop.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee_Shop.Database.Repositories
{
    class BankCardRepositoryPosgreSQL : IRepository<BankCard>
    {
        private ApplicationContext db { get; set; }
        
        #region Constructor 

        public BankCardRepositoryPosgreSQL()
        {
            this.db = ApplicationContext.GetContext();
        }

        #endregion

        #region Methods

        public IEnumerable<BankCard> GetIEnumerable()
        {
            return db.BankCards;
        }

        public BankCard? Get(int id)
        {
            return db.BankCards.Find(id);
        }

        public void Create(BankCard item)
        {
            db.BankCards.Add(item);
        }

        public void Update(BankCard item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            BankCard? bankCard = db.BankCards.Find(id);

            if (bankCard != null)
            {
                db.BankCards.Remove(bankCard);
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
