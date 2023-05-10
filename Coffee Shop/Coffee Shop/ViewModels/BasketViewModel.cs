using Coffee_Shop.Models;
using CoffeeShop.Commands;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Coffee_Shop.ViewModels
{
    internal class BasketViewModel : ViewModelBase
    {
		//private List<ProductFromBasket> products;
		
  //      public List<ProductFromBasket> Products
		//{
		//	get 
		//	{
		//		return products; 
		//	}
		//	set 
		//	{ 
		//		products = value; 
		//	}
		//}
		public BasketViewModel()
		{
			//Products = db.GetProductFromBasketList().ToList();
            //Total = Products.Average(x => x.Product.Price);
        }

        #region Property

        private ProductFromBasket product;

        public ProductFromBasket Product
        {
            get 
            { 
                return product; 
            }
            set 
            {
                product = value;
                MessageBox.Show("GREAT!");
                OnPropertyChanged(nameof(Product)); 
            }
        }

        private decimal total;
        public decimal Total
        {
            get
            {
                return total;
            }
            set
            {
                //total = Products.Average(x => x.Product.Price);
                OnPropertyChanged(nameof(Total));
            }
        }

        #endregion

        #region Command

        #region Delete Product 

        private DelegateCommand? deleteProductCommand;

        public ICommand DeleteProductCommand
        {
            get
            {
                if (deleteProductCommand == null)
                {
                    deleteProductCommand = new DelegateCommand(DeleteProduct);
                }
                return deleteProductCommand;
            }
        }

        private void DeleteProduct()
        {
            MessageBox.Show("Delete");
            //db.DeleteProductFromBasket();
        }




        // удалить наверное
        private ProductFromBasket hoverListViewItem;

        public ProductFromBasket HoverListViewItem
        {
            get
            {
                return hoverListViewItem;
            }
            set
            {
                hoverListViewItem = value;
                MessageBox.Show("GREAT!");
                OnPropertyChanged(nameof(hoverListViewItem));
            }
        }


        #endregion

        #endregion
    }
}
