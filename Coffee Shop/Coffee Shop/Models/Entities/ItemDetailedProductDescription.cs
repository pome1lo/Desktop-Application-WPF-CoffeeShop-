using CoffeShop.Data.Models;
using CoffeShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Windows;

namespace Coffee_Shop.Models.Entities
{
    internal class ItemDetailedProductDescription
    {
        public string? DescriptionName { get; set; } = string.Empty;
        public string? Value { get; set; } = string.Empty;

        public static List<ItemDetailedProductDescription> GetItemDetailedProductDescription(Product product)
        {
            var temp = new List<ItemDetailedProductDescription>();

            for (int i = 1; i < product?.Description?.GetType().GetProperties().Length; i++)
            {
                temp.Add(new ItemDetailedProductDescription()
                {
                    DescriptionName = product?.Description?.GetType().GetProperties()[i].Name,
                    Value = product?.Description?.GetType()?.GetProperty(typeof(Description).GetProperties()[i].Name)?.GetValue(product.Description)?.ToString() + " mg",
                });
            }
            return temp;
        }
    }
}
