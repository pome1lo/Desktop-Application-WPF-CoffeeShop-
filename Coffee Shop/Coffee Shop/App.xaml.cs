using Coffee_Shop.Models;
using Coffee_Shop.ViewModels;
using Coffee_Shop.Views.Pages;
using CoffeeShop.Data.Models;
using CoffeeShop.Views;
using CoffeShop.Data;
using CoffeShop.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Coffee_Shop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static string CultureName = File.ReadAllText(@"../../../StaticFiles/CultureSettings.txt");
        private void OnStartup(object sender, StartupEventArgs e)
        {
            SetLanguage();

            LogIOView logView = new LogIOView();
            LogIOViewModel logIOViewModel = new LogIOViewModel(ref logView);
            logView.DataContext = logIOViewModel;
            logView.Show();
        }

        public static MenuView ConnectionViewModelWithMenu()
        {
            MenuView menuView = new MenuView();
            MenuViewModel menuViewModel = new MenuViewModel();
            menuView.DataContext = menuViewModel;

            return menuView;
        }

        public static AdminView ConnectionViewModelWithAdmin()
        {
            AdminView adminView = new AdminView();
            
            ControlsForAdmin controlsForAdmin = new ControlsForAdmin(
                CultureName,
                adminView.PanelAddNewProduct,
                adminView.PanelOtherParameters,
                adminView.PanelPersonDataBase,
                adminView.PanelProductDataBase
                );

            AdminViewModel adminViewModel = new AdminViewModel(controlsForAdmin);
            adminView.DataContext = adminViewModel;

            return adminView;
        }


        public static void СonfiguringTheMainView(string UserType)
        {
            MainView mainView = new MainView();
            MainViewModel mainViewModel = new MainViewModel(mainView.MainFrame, mainView.AdminPanelButton, CultureName, UserType);// стрем переделать
            mainView.DataContext = mainViewModel;

            mainView.Show();
        }

        private static void SetLanguage()
        {
            Application.Current.Resources.MergedDictionaries.Add(
                new ResourceDictionary()
                {
                    Source = new Uri(
                        String.Format($"StaticFiles/Resources/Lang{CultureName}.xaml"),
                        UriKind.Relative
                    )
                }
            );
        }
    }
}
