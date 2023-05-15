using Coffee_Shop.Models.Entities;
using CoffeShop.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeShop.Models
{
    public class Product
    {
        public int Id { get; set; } = default;
        public string Name { get; set; } = string.Empty;
        public string Image { get; set; } = @"\StaticFiles\Img\ImgDefault.png";
        public decimal Price { get; set; } = default;
        public ushort Calories { get; set; }
        public ProductType ProductType { get; set; }
        public Description Description { get; set; }

        public Product()
        {
            this.Description= new Description();
            this.ProductType = new ProductType();
        }
    }
}
