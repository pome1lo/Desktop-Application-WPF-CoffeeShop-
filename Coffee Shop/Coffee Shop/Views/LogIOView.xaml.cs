using System.Windows;

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

        private void logViewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
    }
}
