using APIForEmail;
using Coffee_Shop.Database;
using Coffee_Shop.Models;
using Coffee_Shop.Views;
using CoffeeShop.Commands;
using DataValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using static DataValidation.Validator;

namespace Coffee_Shop.ViewModels
{
    internal class OrderViewModel : ViewModelBase
    {
        #region Constructor

        public OrderViewModel()
        {
            this.validator = new Validator(this);
            this.Db = new UnitOfWork();
        }

        #endregion

        #region Fields

        private List<ProductFromBasket> products = CurrentUser.ProductsFromBasket;

        private UnitOfWork Db;

        private Address address = new Address();

        private string comment = string.Empty;
        private string errorFlat = string.Empty;
        private string errorIntercom = string.Empty;
        private string errorEntrance = string.Empty;
        private string errorFloor = string.Empty;

        private Validator validator;

        private DelegateCommand<OrderView>? payCommand;
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
                OnPropertyChanged(nameof(Total));
            }
        }

        #endregion

        #region Adress

        public string Flat
        {
            get => address.Flat;
            set
            {
                address.Flat = value;
                OnPropertyChanged(nameof(Flat));
            }
        }

        public string Intercom
        {
            get => address.Intercom;
            set
            {
                address.Intercom = value;
                OnPropertyChanged(nameof(Intercom));
            }
        }

        public string Entrance
        {
            get => address.Entrance;
            set
            {
                address.Entrance = value;
                OnPropertyChanged(nameof(Entrance));
            }
        }

        public string Floor
        {
            get => address.Floor;
            set
            {
                address.Floor = value;
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
                    payCommand = new DelegateCommand<OrderView>((OrderView view) =>
                    {
                        if (IsAdressValidate())
                        {
                            if (CurrentUser.BankCard != null)
                            {
                                SendToModalWindow("The order has been placed!");
                                new Task(CreateOrder).Start();
                                new Task(SendMailToUserAboutOrder).Start();

                                view?.Close();
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

        private void SendMailToUserAboutOrder()
        {
            MailBuilder mail = new MailBuilder("Информация о заказе", TypeOfLetter.Welcome, CurrentUser.Email,
                                $"{CurrentUser.UserName}, ваш заказ оформлен и переведен в статус проверки.");
            mail.Send();
        }

        private void CreateOrder()
        {
            Notification notification = new Notification()
            {
                Content = $"{CurrentUser.UserName}, ваш заказ оформлен и переведен в статус проверки.",
                Date = DateTime.Now,
                Title = "Информация о заказе",
            };

            Order order = new Order()
            {
                Date = DateTime.Now,
                Status = Db.OrderStatuses.GetByName("VALIDATING"),
                Total = Total,
                Comment = this.comment,
                User = CurrentUser,
            };
            Db.Order.Create(order);
            CurrentUser.ProductsFromBasket = new();
            CurrentUser.Notifications.Add(notification);
            CurrentUser.Address = this.address;
            Db.Save();
        }
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
