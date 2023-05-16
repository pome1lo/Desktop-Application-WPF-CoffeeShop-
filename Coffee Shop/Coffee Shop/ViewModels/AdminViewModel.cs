using APIForEmail;
using Coffee_Shop.Database;
using Coffee_Shop.Models;
using Coffee_Shop.Views;
using CoffeeShop.Commands;
using CoffeeShop.Data.Models;
using CoffeShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Coffee_Shop.ViewModels
{
    internal class AdminViewModel : ViewModelBase
    {
        #region Constructors

        public AdminViewModel()
        {
            Db = new UnitOfWork();
            news = Db.News.GetIEnumerable().ToList();
            users = Db.Users.GetIEnumerable().ToList();
            orders = Db.Order.GetIEnumerable().ToList();
            products = Db.Products.GetIEnumerable().ToList();
        }

        #endregion

        #region Fields

        private UnitOfWork Db;

        private List<News> news;
        private List<User> users;
        private List<Product> products;
        private List<Order> orders;

        private News selectedItemForNewsDB;
        private User selectedItemForUsersDB;
        private Product selectedItemForProductsDB;
        private Order selectedItemForOrderDB;

        private DelegateCommand? showAddNewProductCommand;
        private DelegateCommand? showAddNewNewsCommand;
        private DelegateCommand? showAddNewUserCommand;
        private DelegateCommand? deleteProductCommand;
        private DelegateCommand? deleteNewsCommand;
        private DelegateCommand? deleteUserCommand;

        private DelegateCommand? confirmTheOrderCommand;
        private DelegateCommand? rejectTheOrderCommand;

        #endregion

        #region Property

        #region Products list

        public List<Product> Products
        {
            get => products;
            set
            {
                products = value;
                OnPropertyChanged(nameof(Products));
            }
        }

        #endregion

        #region Users list

        public List<User> Users
        {
            get => users;
            set
            {
                users = value;
                OnPropertyChanged(nameof(Users));
            }
        }

        #endregion

        #region News list

        public List<Order> Orders
        {
            get => orders;
            set
            {
                orders = value;
                OnPropertyChanged(nameof(Orders));
            }
        }

        #endregion

        #region News list

        public List<News> News
        {
            get => news;
            set
            {
                news = value;
                OnPropertyChanged(nameof(News));
            }
        }

        #endregion

        #region Selected Item For Users Database

        public User SelectedItemForUsersDB
        {
            get => selectedItemForUsersDB;
            set
            {
                selectedItemForUsersDB = value;
                OnPropertyChanged(nameof(SelectedItemForUsersDB));
            }
        }

        #endregion

        #region Selected Item For OrderDB

        public Order SelectedItemForOrderDB
        {
            get => selectedItemForOrderDB;
            set
            {
                selectedItemForOrderDB = value;
                OnPropertyChanged(nameof(SelectedItemForOrderDB));

            }
        }

        #endregion

        #region Selected Item For news Database

        public News SelectedItemForNewsDB
        {
            get => selectedItemForNewsDB;
            set
            {
                selectedItemForNewsDB = value;
                OnPropertyChanged(nameof(SelectedItemForNewsDB));
            }
        }

        #endregion

        #region Selected Item For Products Database

        public Product SelectedItemForProductsDB
        {
            get => selectedItemForProductsDB;
            set
            {
                selectedItemForProductsDB = value;
                OnPropertyChanged(nameof(SelectedItemForProductsDB));
            }
        }

        #endregion

        #endregion

        #region Commands

        #region Show Add New Product DataBase

        public ICommand ShowAddNewProductCommand
        {
            get
            {
                if (showAddNewProductCommand == null)
                {
                    showAddNewProductCommand = new DelegateCommand(() =>
                    {
                        CreateElement view = new CreateElement();
                        view.NewProduct.Visibility = Visibility.Visible;
                        view.ShowDialog();
                    });
                }
                return showAddNewProductCommand;
            }
        }

        #endregion

        #region Show Add New News DataBase

        public ICommand ShowAddNewNewsCommand
        {
            get
            {
                if (showAddNewNewsCommand == null)
                {
                    showAddNewNewsCommand = new DelegateCommand(() =>
                    {
                        CreateElement view = new CreateElement();
                        view.NewNews.Visibility = Visibility.Visible;
                        view.ShowDialog();
                    });
                }
                return showAddNewNewsCommand;
            }
        }

        #endregion

        #region Show Add New User DataBase

        public ICommand ShowAddNewUserCommand
        {
            get
            {
                if (showAddNewUserCommand == null)
                {
                    showAddNewUserCommand = new DelegateCommand(() =>
                    {
                        CreateElement view = new CreateElement();
                        view.NewUser.Visibility = Visibility.Visible;
                        view.ShowDialog();
                    });
                }
                return showAddNewUserCommand;
            }
        }

        #endregion

        #region Delete product

        public ICommand DeleteProductCommand
        {
            get
            {
                if (deleteProductCommand == null)
                {
                    deleteProductCommand = new DelegateCommand(() =>
                    {
                        if (SelectedItemForProductsDB == null)
                        {
                            SendToModalWindow("First select the item to delete.");
                        }
                        else
                        {
                            Db.Products.Delete(SelectedItemForProductsDB.Id);
                            Db.Save();
                            Products = Db.Products.GetIEnumerable().ToList();
                            SendToModalWindow("The item was successfully deleted.");
                        }
                    });
                }
                return deleteProductCommand;
            }
        }

        #endregion

        #region Delete user

        public ICommand DeleteUserCommand
        {
            get
            {
                if (deleteUserCommand == null)
                {
                    deleteUserCommand = new DelegateCommand(() =>
                    {
                        if (SelectedItemForUsersDB == null)
                        {
                            SendToModalWindow("First select the item to delete.");
                        }
                        else
                        {
                            Db.Users.Delete(selectedItemForUsersDB.Id);
                            Db?.Save();
                            Users = Db.Users.GetIEnumerable().ToList();
                            SendToModalWindow("The item was successfully deleted.");
                        }
                    });
                }
                return deleteUserCommand;
            }
        }

        #endregion

        #region Delete news

        public ICommand DeleteNewsCommand
        {
            get
            {
                if (deleteNewsCommand == null)
                {
                    deleteNewsCommand = new DelegateCommand(() =>
                    {
                        if (SelectedItemForNewsDB == null)
                        {
                            SendToModalWindow("First select the item to delete.");
                        }
                        else
                        {
                            Db.News.Delete(SelectedItemForNewsDB.Id);
                            Db?.Save();
                            News = Db.News.GetIEnumerable().ToList();
                            SendToModalWindow("The item was successfully deleted.");
                        }
                    });
                }
                return deleteNewsCommand;
            }
        }
        #endregion

        #region Confirm The Order 

        public ICommand ConfirmTheOrderCommand
        {
            get
            {
                if (confirmTheOrderCommand == null)
                {
                    confirmTheOrderCommand = new DelegateCommand(() =>
                    {
                        new Task(() =>
                        {
                            SendToMailConfirm(
                            SelectedItemForOrderDB.User.Email,
                            SelectedItemForOrderDB.User.UserName,
                            SelectedItemForOrderDB.Total,
                            SelectedItemForOrderDB.User
                            ); ;
                        }).Start();

                        Orders = Db.Order.GetIEnumerable().ToList();
                    });
                }
                return confirmTheOrderCommand;
            }
        }

        #endregion

        #region Reject The Order

        public ICommand RejectTheOrderCommand
        {
            get
            {
                if (rejectTheOrderCommand == null)
                {
                    rejectTheOrderCommand = new DelegateCommand(() =>
                    {
                        new Task(() =>
                        {
                            SendToMailReject(
                            SelectedItemForOrderDB.User.Email,
                            SelectedItemForOrderDB.User.UserName,
                            SelectedItemForOrderDB.Total,
                            SelectedItemForOrderDB.User
                        );
                        }).Start();

                        Db.Order.Delete(SelectedItemForOrderDB.Id);
                        Db.Save();
                        Orders = Db.Order.GetIEnumerable().ToList();
                    });
                }
                return rejectTheOrderCommand;
            }
        }

        #endregion

        #endregion

        #region Methods

        private void SendToMailConfirm(string email, string name, decimal total, User user)
        {
            string title = "Информация о заказе";
            string content = $"{name}, ваш заказ на сумму {total}$ успешно одобрен администратором.";
            MailBuilder mail = new MailBuilder(title, TypeOfLetter.Welcome, email, content);
            mail.Send();

            Notification notification = new Notification();
            notification.Title = title;
            notification.Content = content;
            notification.Date = DateTime.Now;
            User User = Db.Users.Get(user.Id);
            User.Notifications.Add(notification);
            Db.Order.Delete(SelectedItemForOrderDB.Id);
            Db.Save();
        }
        private void SendToMailReject(string email, string name, decimal total, User user)
        {
            string title = "Информация о заказе";
            string content = $"{name}, ваш заказ на сумму {total}$ был отклонен администратором.";

            MailBuilder mail = new MailBuilder(title, TypeOfLetter.Welcome, email, content);
            mail.Send();

            Notification notification = new Notification();
            notification.Title = title;
            notification.Content = content;
            notification.Date = DateTime.Now;
            User User = Db.Users.Get(user.Id);
            User.Notifications.Add(notification);
            Db.Order.Delete(SelectedItemForOrderDB.Id);
            Db.Save();
        }

        #endregion
    }
}
