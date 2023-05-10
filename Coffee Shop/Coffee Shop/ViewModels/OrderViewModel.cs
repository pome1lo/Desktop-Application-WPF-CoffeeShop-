using Coffee_Shop.Models;
using Coffee_Shop.Views;
using CoffeeShop.Commands;
using DataValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static DataValidation.Validator;

namespace Coffee_Shop.ViewModels
{
    internal class OrderViewModel : ViewModelBase
    {
        #region Constructor

        //public OrderViewModel()
        //{
        //    Products = CurrentUser.ProductsFromBasket;//Db.GetProductFromBasketList().ToList();
        //    //Total = Products.Select(x => x.Product.Price * x.Quantity).Sum();
        //}

        public OrderViewModel()
        {
            this.validator = new Validator(this);
        }

        #endregion

        #region Fields

        private List<ProductFromBasket> products = CurrentUser.ProductsFromBasket;

        private string flat = string.Empty;
        private string intercom = string.Empty;
        private string entrance = string.Empty;
        private string floor = string.Empty;
        private string comment = string.Empty;
        
        private string errorFlat = string.Empty;
        private string errorIntercom = string.Empty;
        private string errorEntrance = string.Empty;
        private string errorFloor = string.Empty;
        private string errorComment = string.Empty;

        private Validator validator;

        private DelegateCommand? payCommand;
        private DelegateCommand? changeBankCard;

        #endregion

        #region Property

        #region Product List

        public List<ProductFromBasket> Products
        {
            get => products;
            set
            {
                products = value;
                OnPropertyChanged(nameof(Products));
            }
        }

        #endregion

        #region Total 

        public decimal Total
        {
            get => Products.Sum(x => x.Product.Price * x.Quantity);
            set
            {
                //total = Products.Sum(x => x.Product.Price * x.Quantity);
                OnPropertyChanged(nameof(Total));
            }
        }

        #endregion

        #region Adress

        public string Flat
        {
            get => flat;
            set
            {
                flat = value;
                OnPropertyChanged(nameof(Flat));
            }
        }
        
        public string Intercom
        {
            get => intercom;
            set
            {
                intercom = value;
                OnPropertyChanged(nameof(Intercom));
            }
        }

        public string Entrance
        {
            get => entrance;
            set
            {
                entrance = value;
                OnPropertyChanged(nameof(Entrance));
            }
        }

        public string Floor
        {
            get => floor;
            set
            {
                floor = value;
                OnPropertyChanged(nameof(Floor));
            }
        }
        
        public string Comment
        {
            get => comment;
            set
            {
                comment = value;
                OnPropertyChanged(nameof(Comment));
            }
        }



        #endregion

        #region Errors

        public string ErrorFlat
        {
            get => errorFlat;
            set
            {
                errorFlat = value;
                OnPropertyChanged(nameof(ErrorFlat));
            }
        }

        public string ErrorIntercom
        {
            get => errorIntercom;
            set
            {
                errorIntercom = value;
                OnPropertyChanged(nameof(ErrorIntercom));
            }
        }

        public string ErrorEntrance
        {
            get => errorEntrance;
            set
            {
                errorEntrance = value;
                OnPropertyChanged(nameof(ErrorEntrance));
            }
        }

        public string ErrorFloor
        {
            get => errorFloor;
            set
            {
                errorFloor = value;
                OnPropertyChanged(nameof(ErrorFloor));
            }
        } 

        #endregion

        #endregion

        #region Command

        #region Change bank card

        public ICommand ChangeBankCard
        {
            get
            {
                if (changeBankCard == null)
                {
                    changeBankCard = new DelegateCommand(() =>
                    {
                        new BankCardView().ShowDialog();
                    });
                }
                return changeBankCard;
            }
        }

        #endregion

        #region Pay 

        public ICommand PayCommand
        {
            get
            {
                if (payCommand == null)
                {
                    payCommand = new DelegateCommand(() =>
                    {
                        if (IsAdressValidate())
                        {
                            if (CurrentUser.BankCard != null)
                            {
                                SendToModalWindow("ВСЕ ЗАЕБОК");
                            }
                            else
                            {
                                SendToModalWindow("You must enter the card details");
                            }
                        }
                    });
                }
                return payCommand;
            }
        }



        #endregion

        #endregion

        #region Methods

        private bool IsAdressValidate()
        {
            return validator.Verify(ValidationBased.OrdinaryDigits, Flat, nameof(ErrorFlat)) &
                validator.Verify(ValidationBased.OrdinaryDigits, Floor, nameof(ErrorFloor)) &
                validator.Verify(ValidationBased.OrdinaryDigits, Entrance, nameof(ErrorEntrance)) &
                validator.Verify(ValidationBased.OrdinaryDigits, Intercom, nameof(ErrorIntercom))
                ;
        }

        #endregion
    }
}
