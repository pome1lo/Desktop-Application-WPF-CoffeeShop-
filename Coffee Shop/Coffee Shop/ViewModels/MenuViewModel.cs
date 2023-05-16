using Coffee_Shop.Database;
using Coffee_Shop.Models;
using Coffee_Shop.Models.Entities;
using Coffee_Shop.Views;
using CoffeeShop.Commands;
using CoffeShop.Models;
using DataValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using static DataValidation.Validator;

namespace Coffee_Shop.ViewModels
{
    internal class MenuViewModel : ViewModelBase
    {
        #region Constructor

        public MenuViewModel()
        {
            this.Db = new UnitOfWork();
            validator = new Validator(this);
            Products = Db.Products.GetIEnumerable().ToList();
        }

        #endregion

        #region Fields

        private string categoryItem = string.Empty;

        private UnitOfWork Db;
        private List<Product>? products;
        private Product? selectedItemForListProducts;
        private Validator validator;

        private string prodNameFroItemCard;
        private string errorRangeFrom = string.Empty;
        private string errorRangeTo = string.Empty;
        private string searchString = string.Empty;
        private string rangeFrom = string.Empty;
        private string rangeTo = string.Empty;
        private int sliderValue = -1;

        private DelegateCommand? findButtonCommand;
        private DelegateCommand? resetButtonCommand;
        private DelegateCommand<object>? plusItemCardCommand;
        private DelegateCommand<object>? minusItemCardCommand;
        private DelegateCommand<object>? closeItemCardCommand;
        private DelegateCommand<object>? addToBasketCommand;
        private DelegateCommand<ProdType> checkedTypeCommand;
        private DelegateCommand? placeAnOrderCommand;

        #endregion

        #region Property

        #region Main List Product

        public List<Product>? Products
        {
            get => products;
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
            }
        }

        #endregion

        #region Selected item name for basket list products

        public string ProdNameFroItemCard
        {
            get => prodNameFroItemCard;
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
            var currentProducts = Db.Products.GetIEnumerable().ToList();
            Products = currentProducts.Where(x => x.Name.ToLower().Contains(searchString.ToLower())).ToList();
        }

        private void RangeFilter()
        {
            if (!string.IsNullOrEmpty(RangeTo))
            {
                Products = Db.Products.GetIEnumerable().Where(x => x.Price < Decimal.Parse(RangeTo)).ToList();
            }
            if (!string.IsNullOrEmpty(RangeFrom))
            {
                Products = Db.Products.GetIEnumerable().Where(x => x.Price > Decimal.Parse(RangeFrom)).ToList();
            }
        }

        private void SliderFilter()
        {
            if (SliderValue != -1)
            {
                Products = Db.Products.GetIEnumerable().Where(x => x.Calories < SliderValue).ToList();
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
                        ProductsFromBasket = new(CurrentUser.ProductsFromBasket);
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
                                Db.ProductsFromBascket.Delete(prod.Id);
                                Db.Save();
                                ProductsFromBasket = new(CurrentUser.ProductsFromBasket);
                            }
                            else
                            {
                                CurrentUser.ProductsFromBasket.Find(x => x == prod).Quantity--;
                            }
                            Db.Save();
                            ProductsFromBasket = new(CurrentUser.ProductsFromBasket);
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
                        Db.ProductsFromBascket.Delete(prod.Id);
                        Db.Save();
                        ProductsFromBasket = new(CurrentUser.ProductsFromBasket); 
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
                        }
                        else
                        {
                            CurrentUser.ProductsFromBasket.Add(new ProductFromBasket() { Product = product });
                        }
                        Db.Save();
                        ProductsFromBasket = new(CurrentUser.ProductsFromBasket);
                    });
                }
                return addToBasketCommand;
            }
        }

        #endregion

        #region Reset

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
                        Products = Db.Products.GetIEnumerable().ToList();
                    });
                }
                return resetButtonCommand;
            }
        }

        #endregion

        #region Checked type
        
        public ICommand CheckedTypeCommand
        {
            get
            {
                if (checkedTypeCommand == null)
                {
                    checkedTypeCommand = new DelegateCommand<ProdType>((ProdType obj) =>
                    {
                        Products = Db.Products.GetIEnumerable().ToList()
                            .Where(x => x.ProductType.Name == obj.ToString())
                            .ToList();
                    });
                }
                return checkedTypeCommand;
            }
        }

        #endregion

        #region Search product
          
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
         
        public ICommand PlaceAnOrderCommand
        {
            get
            {
                if (placeAnOrderCommand == null)
                {
                    placeAnOrderCommand = new DelegateCommand(() =>
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