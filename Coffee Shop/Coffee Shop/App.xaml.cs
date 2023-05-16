using Coffee_Shop.Models.Entities;
using Coffee_Shop.ViewModels;
using Coffee_Shop.Views;
using Coffee_Shop.Views.Pages;
using CoffeeShop.Views;
using CoffeShop.Models;
using DataEncryption;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Coffee_Shop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    /// 

    public partial class App : Application
    {
        public App()
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }
    }
}