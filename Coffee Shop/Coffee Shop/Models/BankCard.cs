using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee_Shop.Models
{
    internal class BankCard
    {
        public int Id { get; set; } = default; 
        public string Number { get; set; } = string.Empty;
        public string CVV { get; set; } = string.Empty;
        public string HolderName { get; set; } = string.Empty;
        public string CardPeriod { get; set; } = string.Empty;
    }
}
