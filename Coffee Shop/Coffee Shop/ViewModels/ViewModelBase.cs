using Coffee_Shop.Database;
using Coffee_Shop.Views;
using Coffee_Shop.Views.Pages;
using CoffeeShop.Data.Models;
using CoffeShop.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using static Coffee_Shop.Database.ApplicationContext;

namespace Coffee_Shop.ViewModels
{
    internal class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected static IRepository? Db { get; private set; }
        protected static User CurrentUser { get; set; } = new();

        private static Frame MainFrame = new();

        protected static void ShowMainWindow()
        {
            MainView view = new MainView();
            MainFrame = view.MainFrame;
            ShowPage(new HomeView());
            view.Show();
        }

        protected static void ShowProductInfoPage(Product? product)
        {
            ProductInfoView view = new ProductInfoView();
            ProductInfoViewModel viewModel = new ProductInfoViewModel(product);
            view.DataContext = viewModel;
            ShowPage(view);
        }


        public static void SetDatabase(byte database)
        {
            SetConnectionString(database);
            Db = GetTheCurrentDatabase();
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler? handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        
        protected static void SendToModalWindow(string content)
        {
            new ModalWindow(content).ShowDialog();
        }

        protected static void ShowPage(Page page) 
        {
            MainFrame.Content = page;
        }
    }
}