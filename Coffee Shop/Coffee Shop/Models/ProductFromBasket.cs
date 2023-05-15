using CoffeeShop.Data.Models;
using CoffeShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee_Shop.Models
{
    class ProductFromBasket
    {
        public int Id { get; set; } = default;
        public Product? Product { get; set; }
        public ushort Quantity { get; set; } = 1;

        public ProductFromBasket()
        {
            this.Product = new Product();
        }
    }
}
