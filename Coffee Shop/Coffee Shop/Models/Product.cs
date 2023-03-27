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
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Img { get; set; } = string.Empty;
        public ushort Calories { get; set; }
        public Description? Description { get; set; }

        public Product()
        {
            Description= new Description();
        }
    }
}
