using Coffee_Shop.Models;
using Coffee_Shop.Models.Entities;
using Coffee_Shop.Views;
using CoffeeShop.Commands;
using CoffeeShop.Data.Models;
using CoffeShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Coffee_Shop.ViewModels
{
    internal class AdminViewModel : ViewModelBase
    {
        #region Constructors

        public AdminViewModel() { }

        public AdminViewModel(string lang, string theme)
        {
            this.language = lang;
            this.theme = theme;
        }

        #endregion

        #region Fields

        private List<News> news = Db.GetNewsList().ToList();
        private List<User> users = Db.GetUserList().ToList();
        private List<Product> products = Db.GetProductList().ToList();

        private string language;
        private string theme;

        private News selectedItemForNewsDB;
        private User selectedItemForUsersDB;
        private Product selectedItemForProductsDB;

        private DelegateCommand? showAddNewProductCommand;
        private DelegateCommand? showAddNewNewsCommand;
        private DelegateCommand? showAddNewUserCommand;
        private DelegateCommand? deleteProductCommand;
        private DelegateCommand? deleteNewsCommand;
        private DelegateCommand? deleteUserCommand;

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

        #region Change language

        public string Language
        {
            get
            {
                return language;
            }
            set
            {
                language = new String(value.Skip(38).ToArray());

                ChangeLanguage();
                OnPropertyChanged(nameof(Language));
            }
        }

        #endregion

        #region Change theme

        public string Theme
        {
            get
            {
                return theme;
            }
            set
            {
                theme = new String(value.Skip(38).ToArray());

                ChangeTheme();
                OnPropertyChanged(nameof(Theme));
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

        #region Methods

        private void ChangeLanguage()
        {
            System.Windows.Application.Current.Resources.MergedDictionaries.Add(
                new ResourceDictionary()
                {
                    Source = new Uri(
                        String.Format($"StaticFiles/Resources/Lang{Language}.xaml"),
                        UriKind.Relative
                    )
                }
            );
        }

        private void ChangeTheme()
        {
            System.Windows.Application.Current.Resources.MergedDictionaries.Add(
                new ResourceDictionary()
                {
                    Source = new Uri(
                        String.Format($"StaticFiles/Themes/{Theme}.xaml"),
                        UriKind.Relative
                    )
                }
            );
        }

        private void ShowPageCreateElementView(TypeOfObjectBeingCreated product)
        {
            throw new NotImplementedException();
        }

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
                        //MainView.IsEnabled = false;удалить
                        //MainView.Opacity = 0.5;
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
                        //MainView.IsEnabled = false;удалить
                        //MainView.Opacity = 0.5;
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
                        //MainView.IsEnabled = false;удалить
                        //MainView.Opacity = 0.5;
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
                            Db.DeleteProduct(SelectedItemForProductsDB.Id);
                            Db.Save();
                            Products = Db.GetProductList().ToList();
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
                            Db?.DeleteUser(selectedItemForUsersDB.Id);
                            Db?.Save();
                            Users = Db.GetUserList().ToList();
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
                            Db?.DeleteNews(SelectedItemForNewsDB.Id);
                            Db?.Save();
                            News = Db.GetNewsList().ToList();
                            SendToModalWindow("The item was successfully deleted.");
                        }
                    });
                }
                return deleteNewsCommand;
            }
        }

        #endregion 

        #endregion
    }
}
