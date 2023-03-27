using Coffee_Shop.Models;
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
        IEnumerable<Person> GetPersonList();
        IEnumerable<Product> GetProductList();
        IEnumerable<News> GetNewsList();
        IEnumerable<Description> GetDescriptionList();
        Person? GetPerson(int id);
        Product? GetProduct(int id);
        Description? GetDescription(int id);
        News? GetNews(int id);
        void CreateProduct(Product item);
        void CreatePerson(Person item);
        void CreateNews(News item);
        void UpdatePerson(Person item);
        void UpdateProduct(Product item);
        void UpdateNews(News item);
        void DeleteProduct(int id);
        void DeletePerson(int id);
        void DeleteNews(int id);
        void Save();
    }
}