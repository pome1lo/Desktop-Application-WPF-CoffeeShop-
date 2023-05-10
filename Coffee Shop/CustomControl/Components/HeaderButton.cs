using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace CustomControl 
{
    [TemplatePart(Name = "img", Type = typeof(Image))]
    public class HeaderButton : UserControl
    {
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(ICommand), typeof(HeaderButton), 
                new PropertyMetadata(null));

        public ImageSource ImgSource
        {
            get { return (ImageSource)GetValue(ImgSourceProperty); }
            set { SetValue(ImgSourceProperty, value);  }
        }

        public static readonly DependencyProperty ImgSourceProperty =
            DependencyProperty.Register("ImgSource", typeof(ImageSource), typeof(HeaderButton));


        private Image img;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            if (this.Template != null)
            {
                img = this.Template.FindName("img", this) as Image;
                img.Source = this.ImgSource;
            }
        }

        #region Constructor

        static HeaderButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(HeaderButton),
              new FrameworkPropertyMetadata(typeof(HeaderButton)));
        } 

        #endregion
    }
}
