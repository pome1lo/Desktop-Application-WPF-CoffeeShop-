using CoffeeShop.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee_Shop.Models
{
    public class Notification
    {
        public int Id { get; set; } = default;
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime Date { get; set; } = DateTime.Now;
        public string Img { get; set; } = @"\StaticFiles\Img\ImgDefault.png";
    }
}
