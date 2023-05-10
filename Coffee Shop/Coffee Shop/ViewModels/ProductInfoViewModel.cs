using Coffee_Shop.Models;
using Coffee_Shop.Models.Entities;
using CoffeeShop.Commands;
using CoffeShop.Data.Models;
using CoffeShop.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Media3D.Converters;

namespace Coffee_Shop.ViewModels
{
    internal class ProductInfoViewModel : ViewModelBase
    {

        /*
         * короче передаем сюда продукт и на его основе показываем все данные
         * при нажатии на кнопку добавить в корзину закидывается в бд с корзиной (созздать)
         * количество тоже фиксирвоать и фэйворит тоже. по умолчанию колво в ноль
         * при плюсике кнопку делать активной. сделать всплывающую кнопку добавления в коризну типа все ок добавилось лайк
        */

        #region Constructor

        public ProductInfoViewModel(Product? product)
        {
            this.Product = new ProductFromBasket()
            {
                Product = product,
            };
            DescriptionList = new List<ItemDetailedProductDescription>(
                ItemDetailedProductDescription.GetItemDetailedProductDescription(product));
        }

        #endregion

        #region Fields

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
                            CurrentUser.ProductsFromBasket.First(x => x.Product == product?.Product).Quantity += 1;
                            //product.Id = Db.GetProductFromBasketList().First(x => x.Product == Product.Product).Id;
                            //(Db.GetProductFromBasket(product.Id) ?? new ProductFromBasket()).Quantity += 1;
                        }
                        else
                        {
                            CurrentUser.ProductsFromBasket.Add(Product);
                            //Db.CreateProductFromBasket(Product);
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
