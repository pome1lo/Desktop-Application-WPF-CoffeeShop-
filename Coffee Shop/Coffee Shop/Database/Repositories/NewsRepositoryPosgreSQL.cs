using Coffee_Shop.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee_Shop.Database.Repositories
{
    class NewsRepositoryPosgreSQL : IRepository<News>
    {
        private ApplicationContext db { get; set; }

        #region Constructor 

        public NewsRepositoryPosgreSQL()
        {
            this.db = ApplicationContext.GetContext();
        }

        #endregion

        #region Methods

        public IEnumerable<News> GetIEnumerable()
        {
            return db.News;
        }

        public News? Get(int id)
        {
            return db.News.Find(id);
        }

        public void Create(News item)
        {
            db.News.Add(item);
        }

        public void Update(News item)
        {
            db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            News? news = db.News.Find(id);

            if (news != null)
            {
                db.News.Remove(news);
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
