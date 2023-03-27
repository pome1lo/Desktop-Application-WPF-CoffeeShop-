using Coffee_Shop.Models;
using Coffee_Shop.Models.Entities;
using Coffee_Shop.ViewModels;
using Coffee_Shop.Views.Pages;
using CoffeeShop.Data.Models;
using CoffeeShop.Views;
using CoffeShop.Data;
using CoffeShop.Data.Models;
using CoffeShop.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Serialization;
using static Coffee_Shop.Database.ApplicationContext;

namespace Coffee_Shop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    /// 

    // SMTP client отправка на почту метанит 

    public partial class App : Application
    {
        //private static string CultureName = File.ReadAllText(@"../../../StaticFiles/CultureSettings.txt");
        //private static string Theme = File.ReadAllText(@"../../../StaticFiles/ThemeSettings.txt");

        private static ConfigurationSettings? Configuration { get; set; } = new();
        private static Frame MainFrame { get; set; } = new();

        private void OnStartup(object sender, StartupEventArgs e)
        {
            Configuration = DataProcessing.GetConfigurationSettings();
            Configuration.SetDefaultSettings();


            // ==== РАЗКОМЕНТИТЬ

            //LogIOView view = new LogIOView();
            //LogIOViewModel viewModel = new LogIOViewModel(ref view);
            //view.DataContext = viewModel;
            //view.Show();

            ConnectionTheMainView("Admin");
        }

        public static void ConnectionViewModelWithMenu()
        {
            MenuView view = new MenuView();
            MenuViewModel viewModel = new MenuViewModel();
            view.DataContext = viewModel;

            MainFrame.Content = view;
        }

        public static void ConnectionViewModelWithAdmin()
        {
            AdminView view = new AdminView();

            ControlsForAdmin controlsForAdmin = new ControlsForAdmin();
                //Configuration.Language,
                //Configuration.Theme,
                //view.PanelAddNewProduct,
                //view.PanelOtherParameters,
                //view.PanelPersonDataBase,
                //view.PanelProductDataBase
                //);

            AdminViewModel viewModel = new AdminViewModel(controlsForAdmin);
            view.DataContext = viewModel;

            MainFrame.Content = view;
        }


        public static void ConnectionTheMainView(string UserType)
        {
            MainView view = new MainView();
            MainViewModel viewModel = new MainViewModel(view.MainFrame, view.AdminPanelButton, Configuration.Language, UserType);// стрем переделать
            view.DataContext = viewModel;

            view.Show();

            MainFrame = view.MainFrame;
        }

        public static void ConnectionTheProductInfoViewModel(Product product)
        {
            ProductInfoView view = new ProductInfoView();
            ProductInfoViewModel viewModel = new ProductInfoViewModel(product);
            view.DataContext = viewModel;

            MainFrame.Content = view;
        }

        public static void ConnectionTheHomeViewModel()
        {
            HomeView view = new HomeView();
            HomeViewModel viewModel = new HomeViewModel();
            view.DataContext = viewModel;

            MainFrame.Content = view;
        }
    }
}
