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

        private void SignInStartPageClick(object sender, RoutedEventArgs e)
        {
            SignInStartPage.Visibility = Visibility.Collapsed;
            JoinStartPage.Visibility = Visibility.Collapsed;
            BlackPanel.SetValue(Grid.RowSpanProperty, 2);
            BlackPanel.SetValue(Grid.RowProperty, 1);
            BlackPanel.Height = 266;

            SignInForm.Visibility = Visibility.Visible;
        }

        private void JoinStartPageClick(object sender, RoutedEventArgs e)
        {
            SignInStartPage.Visibility = Visibility.Collapsed;
            JoinStartPage.Visibility = Visibility.Collapsed;
            BlackPanel.SetValue(Grid.RowSpanProperty, 2);
            BlackPanel.SetValue(Grid.RowProperty, 1);
            BlackPanel.Height = 266;

            JoinForm.Visibility = Visibility.Visible;
        }
    }
}
