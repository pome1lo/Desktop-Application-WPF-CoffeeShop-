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
        public string Vkontakte { get; set; } = string.Empty;
        public string Instagram { get; set; } = string.Empty;
        public string Telegram { get; set; } = string.Empty;
    }
}
