using Coffee_Shop.Database.Repositories;
using System;

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
        private OrderRepositoryPostgreSQL? orderRepository;
        private OrderStatusRepositoruPostgreSQL? orderStatusRepository;
        private AddressRepositoryPostgreSQL? addressRepository;

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
        public OrderRepositoryPostgreSQL Order
        {
            get
            {
                if (orderRepository == null)
                {
                    orderRepository = new OrderRepositoryPostgreSQL();
                }
                return orderRepository;
            }
        }
        public OrderStatusRepositoruPostgreSQL OrderStatuses
        {
            get
            {
                if (orderStatusRepository == null)
                {
                    orderStatusRepository = new OrderStatusRepositoruPostgreSQL();
                }
                return orderStatusRepository;
            }
        }
        public AddressRepositoryPostgreSQL Address
        {
            get
            {
                if (addressRepository == null)
                {
                    addressRepository = new AddressRepositoryPostgreSQL();
                }
                return addressRepository;
            }
        }

        #endregion

        #region Methods

        private object obj = new();
        public void Save()
        {
            lock (obj)
            {
                db.SaveChanges();
            }
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