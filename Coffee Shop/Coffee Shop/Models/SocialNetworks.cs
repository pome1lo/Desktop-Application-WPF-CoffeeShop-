using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee_Shop.Models
{
    public class SocialNetworks
    {
        public int Id { get; set; } = default;
        public string Vkontakte { get; set; } = "https://vk.com/";
        public string Instagram { get; set; } = "https://www.instagram.com";
        public string Telegram { get; set; } = "https://web.telegram.org";
    }
}
