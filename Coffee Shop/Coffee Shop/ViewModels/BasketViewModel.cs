using Coffee_Shop.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee_Shop.ViewModels
{
    internal class BasketViewModel : ViewModelBase
    {
		private List<ProductFromBasket> products;

		public List<ProductFromBasket> Products
		{
			get 
			{
				return products; 
			}
			set 
			{ 
				products = value; 
			}
		}

	}
}
