using CoffeShop.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop.Data.Models
{
    class BasketWithProducts
    {
        public BasketWithProducts(Product product) => _product = product;

        private Product _product = new();
        public Product product
        {
            get => _product;
        }
        public int count { get; set; } = 1;
    }
}
