using CoffeeShop.Data.Models;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee_Shop.Models
{
    internal class Order
    {
        public int Id { get; set; } = default;
        public decimal Total { get; set; } = 0;
        public User? User { get; set; } 
        public DateTime Date { get; set; } = DateTime.Now;
        public OrderStatus? Status { get; set; }
        public string Comment { get; set; } = string.Empty;
    }
}
