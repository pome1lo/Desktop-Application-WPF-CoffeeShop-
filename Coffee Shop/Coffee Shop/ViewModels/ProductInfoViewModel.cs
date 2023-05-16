using Coffee_Shop.Database;
using Coffee_Shop.Models;
using Coffee_Shop.Models.Entities;
using CoffeeShop.Commands;
using CoffeShop.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Coffee_Shop.ViewModels
{
    internal class ProductInfoViewModel : ViewModelBase
    {
        #region Constructor

        public ProductInfoViewModel(Product? product)
        {
            Db = new UnitOfWork();

            this.Product = new ProductFromBasket()
            {
                Product = product,
            };
            DescriptionList = new List<ItemDetailedProductDescription>(
                ItemDetailedProductDescription.GetItemDetailedProductDescription(product));
        }

        #endregion

        #region Fields

        private UnitOfWork Db;

        private ProductFromBasket? product;
        private DelegateCommand? addToBasketCommand;
        private List<ItemDetailedProductDescription>? descriptionList;

        #endregion

        #region Property

        public ProductFromBasket? Product
        {
            get => product;
            set
            {
                product = value;
                OnPropertyChanged(nameof(Product));
            }
        }

        public List<ItemDetailedProductDescription>? DescriptionList
        {
            get => descriptionList;
            set
            {
                descriptionList = value;
                OnPropertyChanged(nameof(DescriptionList));
            }
        }

        #endregion

        #region Command

        #region Add to basket 

        public ICommand AddToBasketCommand
        {
            get
            {
                if (addToBasketCommand == null)
                {
                    addToBasketCommand = new DelegateCommand(() =>
                    {
                        if (CurrentUser.ProductsFromBasket.Any(x => x.Product == Product?.Product))
                        {
                            if (CurrentUser.ProductsFromBasket.First(x => x.Product == product?.Product).Quantity + 1 < 10)
                            {
                                SendToModalWindow("The product has been successfully added to the cart");
                                CurrentUser.ProductsFromBasket.First(x => x.Product == product?.Product).Quantity += 1;
                                Db.Save();
                            }   
                            else
                            {
                                SendToModalWindow("Exceeded the number of units of the product");
                            }
                        }
                        else
                        {
                            if (CurrentUser.ProductsFromBasket.Count() < 5)
                            {
                                SendToModalWindow("The product has been successfully added to the cart");
                                CurrentUser.ProductsFromBasket.Add(Product);
                            }
                        }
                        Db.Save();
                    });
                }
                return addToBasketCommand;
            }
        }

        #endregion

        #endregion
    }
}
