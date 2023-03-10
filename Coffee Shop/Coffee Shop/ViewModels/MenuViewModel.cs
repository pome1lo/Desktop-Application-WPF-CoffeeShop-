using Coffee_Shop.Views.Pages;
using CoffeeShop.Commands;
using CoffeeShop.Data.Models;
using CoffeShop.Data;
using CoffeShop.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Coffee_Shop.ViewModels
{
    internal class MenuViewModel : ViewModelBase
    {
        private ObservableCollection<Product> products { get; set; } = GetProductsFromTheDatabase();

        public ObservableCollection<Product> Products
        {
            get
            {
                return products;
            }
            set
            {
                products = value;
                OnPropertyChanged(nameof(Products));
            }
        }

    private static ObservableCollection<Product> GetProductsFromTheDatabase()
        {
            var Products = new ObservableCollection<Product>();
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Products.ToList().ForEach(x => Products.Add(x));
                Products.ToList().ForEach(x => x.Description = db.Descriptions.ToList()[x.Id - 1]); 
            }
            return Products;
        }

        #region Property

        #region Search
        private string searchString;
        public string SearchString
        {
            get
            {
                return searchString;
            }
            set
            {
                searchString = value;
                Sort();
                OnPropertyChanged(nameof(SearchString));
            }
        }
        #endregion

        #region From/To

        private string rangeFrom = "From";
        public string RangeFrom
        {
            get
            {
                return rangeFrom;
            }
            set
            {
                rangeFrom = value;
                OnPropertyChanged(nameof(RangeFrom));
            }
        }

        private string rangeTo = "To";
        public string RangeTo
        {
            get
            {
                return rangeTo;
            }
            set
            {
                rangeTo = value;
                MessageBox.Show(rangeTo);
                OnPropertyChanged(nameof(RangeTo));
            }
        }

        #endregion

        #region Category

        private string categoryItem = string.Empty;
        public string CategoryItem
        {
            get
            {
                return categoryItem;
            }
            set
            {
                categoryItem = value;
                OnPropertyChanged(nameof(CategoryItem));
            }
        }

        #endregion

        #region SliderValue

        private int sliderValue = -1;

        public int SliderValue
        {
            get 
            { 
                return sliderValue; 
            }
            set 
            {
                sliderValue = value;
                OnPropertyChanged(nameof(SliderValue));
            }
        }



        #endregion

        #endregion

        private void Sort()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var currentProducts = ApplicationContext.GetContext().Products.ToList();
                currentProducts = currentProducts.Where(x => x.Name.ToLower().Contains(searchString.ToLower())).ToList();
                Products = new ObservableCollection<Product>(currentProducts);
            }
        }


        #region Command

        private DelegateCommand? findButtonCommand;

        public ICommand FindButtonCommand
        {
            get
            {
                if (findButtonCommand == null)
                {
                    findButtonCommand = new DelegateCommand(FindButton);
                }
                return findButtonCommand;
            }
        }

        private void FindButton()
        {
            Products = GetSearchProducts();
        }

        private ObservableCollection<Product> GetSearchProducts() // implement via Decorator
        {
            var temp = ApplicationContext.GetContext().Products.ToList();

            if (SliderValue != -1)
            {
                temp = temp.Where(x => x.Calories < SliderValue).ToList();
            }
            if(CategoryItem != string.Empty)
            {
                //temp = temp.Where(x => x.Description)
            }
            if(rangeFrom != "From")
            {
                temp = temp.Where(x => x.Price > Decimal.Parse(rangeFrom)).ToList();
            }
            if (rangeTo != "To")
            {
                temp = temp.Where(x => x.Price < Decimal.Parse(rangeTo)).ToList();
            }
            return new ObservableCollection<Product>(temp);
        }

        #endregion
    }
}
