using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Coffee_Shop.Views
{
    /// <summary>
    /// Логика взаимодействия для ModalWindow.xaml
    /// </summary>
    public partial class ModalWindow : Window
    {
        public ModalWindow(string content)
        {
            InitializeComponent();
            this.textBlock.Text = content;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
