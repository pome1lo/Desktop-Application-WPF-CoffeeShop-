using Coffee_Shop.Models;
using Coffee_Shop.Models.Entities;
using CoffeeShop.Data.Models;
using CoffeShop.Data.Models;
using CoffeShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee_Shop.Database
{
    internal interface IRepository
    {
        IEnumerable<User> GetUserList();
        IEnumerable<Product> GetProductList();
        IEnumerable<News> GetNewsList();
        IEnumerable<Description> GetDescriptionList();
        IEnumerable<ProductFromBasket> GetProductFromBasketList();
        IEnumerable<BankCard> GetBankCardList();
        User? GetUser(int id);
        Product? GetProduct(int id);
        Description? GetDescription(int id);
        News? GetNews(int id);
        ProductFromBasket? GetProductFromBasket(int product);
        BankCard? GetBankCard(int id);
        void CreateProduct(Product item);
        void CreateUser(User item);
        void CreateNews(News item);
        void CreateProductFromBasket(ProductFromBasket item);
        void CreateBankCard(BankCard item);
        void UpdateUser(User item);
        void UpdateProduct(Product item);
        void UpdateNews(News item);
        void UpdateProductFromBasket(ProductFromBasket item);
        void UpdateBankCard(BankCard item);
        void DeleteProduct(int id);
        void DeleteUser(int id);
        void DeleteNews(int id);
        void DeleteProductFromBasket(int id);
        void DeleteBankCard(int id);
        ProductType GetProductType(string Name);
        void Save();
    }
}