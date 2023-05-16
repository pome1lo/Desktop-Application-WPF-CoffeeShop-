using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee_Shop.Models
{
    internal class Address
    {
        public int Id { get; set; } = default;
        public string Flat {get; set;} = string.Empty;
        public string Intercom {get; set;} = string.Empty;
        public string Entrance {get; set;} = string.Empty;
        public string Floor {get; set;} = string.Empty;
    }
}
