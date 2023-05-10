using Coffee_Shop.Database;
using Coffee_Shop.Models;
using Coffee_Shop.Models.Entities;
using Coffee_Shop.Views;
using Coffee_Shop.Views.Pages;
using CoffeeShop.Commands;
using CoffeShop.Models;
using DataValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static DataValidation.Validator;

namespace Coffee_Shop.ViewModels
{
    internal class MenuViewModel : ViewModelBase
    {
        #region Constructor

        public MenuViewModel()
        {
            Products = Db.GetProductList().ToList();
            validator = new Validator(this);
        }

        #endregion

        #region Fields

        private string categoryItem = string.Empty;

        private List<Product>? products;
        private Product? selectedItemForListProducts;
        private Validator validator;

        private string errorRangeFrom = string.Empty;
        private string errorRangeTo = string.Empty;
        private string searchString = string.Empty;
        private string rangeFrom = string.Empty;
        private string rangeTo = string.Empty;
        private int sliderValue = -1;

        private DelegateCommand<object>? plusItemCardCommand;
        private DelegateCommand<object>? minusItemCardCommand;
        private DelegateCommand<object>? closeItemCardCommand;
        private DelegateCommand<object>? addToBasketCommand;

        #endregion

        #region Property

        #region Main List Product

        public List<Product>? Products
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

        #endregion

        #region List Product From Basket

        public List<ProductFromBasket> ProductsFromBasket
        {
            get => CurrentUser.ProductsFromBasket;
            set
            {
                CurrentUser.ProductsFromBasket = value;
                OnPropertyChanged(nameof(ProductsFromBasket));
            }
        }

        #endregion

        #region Search

        public string SearchString
        {
            get => searchString;
            set
            {
                searchString = value;
                Sort();
                OnPropertyChanged(nameof(SearchString));
            }
        }

        #endregion

        #region From/To

        public string RangeFrom
        {
            get => rangeFrom;
            set
            {
                rangeFrom = value;
                validator.Verify(ValidationBased.OrdinaryDigits, RangeFrom, nameof(ErrorRangeFrom));
                OnPropertyChanged(nameof(RangeFrom));
            }
        }

        public string RangeTo
        {
            get => rangeTo;
            set
            {
                rangeTo = value;
                validator.Verify(ValidationBased.OrdinaryDigits, RangeTo, nameof(ErrorRangeTo));
                OnPropertyChanged(nameof(RangeTo));
            }
        }

        #region Errors


        public string ErrorRangeFrom
        {
            get => errorRangeFrom;
            set
            {
                errorRangeFrom = value;
                OnPropertyChanged(nameof(ErrorRangeFrom));
            }
        }

        public string ErrorRangeTo
        {
            get => errorRangeTo;
            set
            {
                errorRangeTo = value;
                OnPropertyChanged(nameof(ErrorRangeTo));
            }
        }


        #endregion

        #endregion

        #region Category

        public string CategoryItem
        {
            get => categoryItem;
            set
            {
                categoryItem = value;
                OnPropertyChanged(nameof(CategoryItem));
            }
        }

        #endregion

        #region SliderValue

        public int SliderValue
        {
            get => sliderValue;
            set
            {
                sliderValue = value;
                OnPropertyChanged(nameof(SliderValue));
            }
        }

        #endregion

        #region Selected Item For List Products

        public Product? SelectedItemForListProducts
        {
            get => null;
            set
            {
                ShowProductInfoPage(value);
                OnPropertyChanged(nameof(SelectedItemForListProducts));
                //App.ConnectionTheProductInfoViewModel(SelectedItemForListProducts);
            }
        }

        #endregion

        #region Selected item name for basket list products

        private string prodNameFroItemCard;

        public string ProdNameFroItemCard
        {
            get
            {
                return prodNameFroItemCard;
            }
            set
            {
                prodNameFroItemCard = value;
                OnPropertyChanged(nameof(ProdNameFroItemCard));
            }
        }

        #endregion

        #endregion

        #region Methods

        private void Sort()
        {
            var currentProducts = Db.GetProductList().ToList();
            Products = currentProducts.Where(x => x.Name.ToLower().Contains(searchString.ToLower())).ToList();
        }


        private void RangeFilter()
        {
            if (!string.IsNullOrEmpty(RangeTo))
            {
                Products = Db.GetProductList().Where(x => x.Price < Decimal.Parse(RangeTo)).ToList();
            }
            if (!string.IsNullOrEmpty(RangeFrom))
            {
                Products = Db.GetProductList().Where(x => x.Price > Decimal.Parse(RangeFrom)).ToList();
            }
        }

        private void SliderFilter()
        {
            if (SliderValue != -1)
            {
                Products = Db.GetProductList().Where(x => x.Calories < SliderValue).ToList();
            }
        }

        #endregion

        #region Command


        #region Buttons for item card 


        public ICommand PlusItemCardCommand
        {
            get
            {
                if (plusItemCardCommand == null)
                {
                    plusItemCardCommand = new DelegateCommand<object>((object obj) =>
                    {
                        var prod = obj as ProductFromBasket;
                        CurrentUser.ProductsFromBasket.Find(x => x == prod).Quantity++;
                        Db.Save();
                        ProductsFromBasket = new(CurrentUser.ProductsFromBasket);//Db.GetProductFromBasketList().ToList();
                    });
                }
                return plusItemCardCommand;
            }
        }


        public ICommand MinusItemCardCommand
        {
            get
            {
                if (minusItemCardCommand == null)
                {
                    minusItemCardCommand = new DelegateCommand<object>((object obj) =>
                    {
                        var prod = obj as ProductFromBasket;
                        ProductFromBasket? prodFromDB = CurrentUser.ProductsFromBasket.Find(x => x == prod);
                        
                        if (prodFromDB != null)
                        {
                            if (prodFromDB.Quantity == 1)
                            {
                                Db.DeleteProductFromBasket(prod.Id);
                                Db.Save();
                                ProductsFromBasket = new(CurrentUser.ProductsFromBasket);//Db.GetProductFromBasketList().ToList();
                            }
                            else
                            {
                                CurrentUser.ProductsFromBasket.Find(x => x == prod).Quantity--;
                            }
                            Db.Save();
                            ProductsFromBasket = new(CurrentUser.ProductsFromBasket);//Db.GetProductFromBasketList().ToList();
                        }
                    });
                }
                return minusItemCardCommand; ;
            }
        }


        public ICommand CloseItemCardCommand
        {
            get
            {
                if (closeItemCardCommand == null)
                {
                    closeItemCardCommand = new DelegateCommand<object>((object obj) =>
                    {
                        var prod = obj as ProductFromBasket;
                        Db.DeleteProductFromBasket(prod.Id);
                        Db.Save();
                        ProductsFromBasket = new(CurrentUser.ProductsFromBasket);//Db.GetProductFromBasketList().ToList();
                    });
                }
                return closeItemCardCommand;
            }
        }

        #endregion

        #region Add product from bascket

        public ICommand AddToBasketCommand
        {
            get
            {
                if (addToBasketCommand == null)
                {
                    addToBasketCommand = new DelegateCommand<object>((object obj) =>
                    {
                        
                        Product? product = obj as Product;
                        if (CurrentUser.ProductsFromBasket.Any(x => x.Product == product))
                        {
                            CurrentUser.ProductsFromBasket.First(x => x.Product == product).Quantity += 1;
                            //product.Id = Db.GetProductFromBasketList().First(x => x.Product == Product.Product).Id;
                            //(Db.GetProductFromBasket(product.Id) ?? new ProductFromBasket()).Quantity += 1;
                        }
                        else
                        {
                            CurrentUser.ProductsFromBasket.Add(new ProductFromBasket() { Product = product });
                            //Db.CreateProductFromBasket(Product);
                        }
                        Db.Save();
                        ProductsFromBasket = new(CurrentUser.ProductsFromBasket);//Db.GetProductFromBasketList().ToList();
                    });
                }
                return addToBasketCommand;
            }
        }

        #endregion

        #region Reset

        private DelegateCommand? resetButtonCommand;

        public ICommand ResetButtonCommand
        {
            get
            {
                if (resetButtonCommand == null)
                {
                    resetButtonCommand = new DelegateCommand(() =>
                    {
                        RangeFrom = "";
                        RangeTo = "";
                        ErrorRangeFrom = string.Empty;
                        ErrorRangeTo = string.Empty;
                        SliderValue = -1;
                        SearchString = string.Empty;
                        Products = Db.GetProductList().ToList();
                    });
                }
                return resetButtonCommand;
            }
        }

        #endregion


        #region Checked type

        private DelegateCommand<ProdType> checkedTypeCommand;
        public ICommand CheckedTypeCommand
        {
            get
            {
                if (checkedTypeCommand == null)
                {
                    checkedTypeCommand = new DelegateCommand<ProdType>((ProdType obj) =>
                    {
                        Products = Db.GetProductList().ToList().Where(x => x.ProductType.Name == obj.ToString()).ToList();
                    });
                }
                return checkedTypeCommand;
            }
        }

        #endregion

        #region Search product


        private DelegateCommand? findButtonCommand;

        public ICommand FindButtonCommand
        {
            get
            {
                if (findButtonCommand == null)
                {
                    findButtonCommand = new DelegateCommand(() =>
                    { 
                        SliderFilter();
                        RangeFilter();
                    });
                }
                return findButtonCommand;
            }
        }

        

        #endregion

        #region Place an order 

        private DelegateCommand? placeAnOrderCommand;

        public ICommand PlaceAnOrderCommand
        {
            get
            {
                if (placeAnOrderCommand == null)
                {
                    placeAnOrderCommand = new DelegateCommand(/*App.ConnectionTheOrderViewModel*/() =>
                    {
                        new OrderView().ShowDialog();
                    });
                }
                return placeAnOrderCommand;
            }
        }

        #endregion

        #endregion
    }
}