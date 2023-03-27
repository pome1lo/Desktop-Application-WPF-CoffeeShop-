using Coffee_Shop;
using CoffeeShop.Data.Models;
using CoffeShop.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CoffeeShop.Views
{
    /// <summary>
    /// Логика взаимодействия для LogIOView.xaml
    /// </summary>
    public partial class LogIOView : Window
    {
        public LogIOView()
        {
            InitializeComponent();
        }

        private void RollUp(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
    }
}
