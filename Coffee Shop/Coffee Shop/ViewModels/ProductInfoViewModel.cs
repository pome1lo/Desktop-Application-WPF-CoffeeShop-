using Coffee_Shop.Models;
using Coffee_Shop.Models.Entities;
using CoffeShop.Data.Models;
using CoffeShop.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Coffee_Shop.ViewModels
{
    internal class ProductInfoViewModel : ViewModelBase
    {
        private ProductFromBasket product { get; set; }
        public ProductFromBasket Product
        {
            get
            {
                return product;
            }
            set
            {
                product = value;
                OnPropertyChanged(nameof(Product));
            }
        }

        /*
         * короче передаем сюда продукт и на его основе показываем все данные
         * при нажатии на кнопку добавить в корзину закидывается в бд с корзиной (созздать)
         * количество тоже фиксирвоать и фэйворит тоже. по умолчанию колво в ноль
         * при плюсике кнопку делать активной. сделать всплывающую кнопку добавления в коризну типа все ок добавилось лайк
        */

        public ProductInfoViewModel(Product product)
        {
            this.Product = new ProductFromBasket()
            {
                Product = product,
            };
            DescriptionList = new ObservableCollection<ItemDetailedProductDescription>(ItemDetailedProductDescription.GetItemDetailedProductDescription(product));
        }

        private ObservableCollection<ItemDetailedProductDescription>? descriptionList { get; set; }

        public ObservableCollection<ItemDetailedProductDescription>? DescriptionList
        {
            get
            {
                return descriptionList;
            }
            set
            {
                descriptionList = value;
                OnPropertyChanged(nameof(DescriptionList));
            }
        }

        /*
         
        * выгести класс тест в Models.Entity пееремименовать 
         
         */

    }
}
