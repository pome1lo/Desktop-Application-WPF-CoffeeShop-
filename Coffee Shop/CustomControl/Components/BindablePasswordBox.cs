using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace CustomControl
{

    [TemplatePart(Name = "passwordBox", Type = typeof(PasswordBox))]
    public partial class BindablePasswordBox : UserControl
    {
        #region Property

        public string Password
        {
            get
            {
                return (string)GetValue(PasswordProperty);
            }
            set
            {
                SetValue(PasswordProperty, value);
            }
        }

        #endregion

        #region Dependecy Property

        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.Register("Password", typeof(string), typeof(BindablePasswordBox),
                new PropertyMetadata(string.Empty));

        #endregion

        #region Constructor

        static BindablePasswordBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(BindablePasswordBox),
                new FrameworkPropertyMetadata(typeof(BindablePasswordBox)));
        }

        #endregion

        private void PasswordBoxPasswordChanged(object sender, RoutedEventArgs e)
        {
            Password = passwordBox.Password;
        }


        private PasswordBox passwordBox;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            if (this.Template != null)
            {
                passwordBox = this.Template.FindName("passwordBox", this) as PasswordBox;
                passwordBox.PasswordChanged += PasswordBoxPasswordChanged;
            }
        }
    }
}
