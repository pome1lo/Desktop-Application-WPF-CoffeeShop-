using Coffee_Shop.Views;
using Coffee_Shop.Views.Pages;
using CoffeeShop.Data.Models;
using CoffeShop.Models;
using System.ComponentModel;
using System.Windows.Controls;

namespace Coffee_Shop.ViewModels
{
    internal class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler? handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }



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
        protected static void ShowPage(Page page) => MainFrame.Content = page;
        protected static void SendToModalWindow(string content) => new ModalWindow(content).ShowDialog();
    }
}


