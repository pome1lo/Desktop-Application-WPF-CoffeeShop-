using Coffee_Shop.Database.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee_Shop.Database
{
    internal class UnitOfWork : IDisposable
    {
        #region Fields

        private ApplicationContext db = ApplicationContext.GetContext();

        private NewsRepositoryPosgreSQL? newsRepository;
        private UserRepositoryPosgreSQL? userRepository;
        private ProductRepositoryPosgreSQL? productRepository;
        private BankCardRepositoryPosgreSQL? bankCardRepository;
        private DescriptionRepositoryPosgreSQL? descriptionRepository;
        private ProductTypeRepositoryPostgreSQL? productTypeRepository;
        private ProductFromBascketRepositoryPosgreSQL? productFromBascketRepository;

        #endregion

        #region Properties

        public NewsRepositoryPosgreSQL News
        {
            get
            {
                if (newsRepository == null)
                {
                    newsRepository = new NewsRepositoryPosgreSQL();
                }
                return newsRepository;
            }
        }
        public UserRepositoryPosgreSQL Users
        {
            get
            {
                if (userRepository == null)
                {
                    userRepository = new UserRepositoryPosgreSQL();
                }
                return userRepository;
            }
        }
        public ProductRepositoryPosgreSQL Products
        {
            get
            {
                if (productRepository == null)
                {
                    productRepository = new ProductRepositoryPosgreSQL();
                }
                return productRepository;
            }
        }
        public BankCardRepositoryPosgreSQL BankCards
        {
            get
            {
                if (bankCardRepository == null)
                {
                    bankCardRepository = new BankCardRepositoryPosgreSQL();
                }
                return bankCardRepository;
            }
        }
        public DescriptionRepositoryPosgreSQL Descriptions
        {
            get
            {
                if (descriptionRepository == null)
                {
                    descriptionRepository = new DescriptionRepositoryPosgreSQL();
                }
                return descriptionRepository;
            }
        }
        public ProductTypeRepositoryPostgreSQL ProductTypes
        {
            get
            {
                if (productTypeRepository == null)
                {
                    productTypeRepository = new ProductTypeRepositoryPostgreSQL();
                }
                return productTypeRepository;
            }
        }
        public ProductFromBascketRepositoryPosgreSQL ProductsFromBascket
        {
            get
            {
                if (productFromBascketRepository == null)
                {
                    productFromBascketRepository = new ProductFromBascketRepositoryPosgreSQL();
                }
                return productFromBascketRepository;
            }
        }

        #endregion

        #region Methods

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
                this.disposed = true;
            }
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
