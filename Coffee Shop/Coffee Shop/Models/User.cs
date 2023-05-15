using Coffee_Shop.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop.Data.Models
{
    class User
    {
        public int Id { get; set; } = default;
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Picture { get; set; } = @"\StaticFiles\Img\ProfileDefault.jpg";
        public string Email { get; set; } = string.Empty;
        public bool IsAdmin { get; set; } = false;
        public string Theme { get; set; } = "Dark";
        public string Language { get; set; } = ".en-US";

        public BankCard? BankCard { get; set; }
        public SocialNetworks? SocialNetworks { get; set; }
        public List<Notification>? Notifications { get; set; }
        public List<ProductFromBasket>? ProductsFromBasket { get; set; }
    }
}
