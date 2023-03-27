using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeShop.Data.Models
{
    public class Description
    {
        public int Id { get; set; }
        public ushort TotalFat { get; set; }
        public ushort SaturatedFat { get; set; }
        public ushort TransFat { get; set; }
        public ushort Cholesterol { get; set; }
        public ushort Sodium { get; set; }
        public ushort TotalCarbohydrates { get; set; }
        public ushort Protein { get; set; }
        public ushort Caffeine { get; set; }

    }
}
