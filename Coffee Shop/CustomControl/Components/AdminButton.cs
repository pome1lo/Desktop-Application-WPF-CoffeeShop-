using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CustomControl
{
    [TemplatePart(Name = "radioButton", Type = typeof(RadioButton))]
    [TemplatePart(Name = "textBlock", Type = typeof(TextBlock))]
    [TemplatePart(Name = "img", Type = typeof(Image))]
    public class AdminButton : UserControl
    {
        #region Constructor

        static AdminButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(AdminButton),
                new FrameworkPropertyMetadata(typeof(AdminButton)));
        }

        #endregion

        #region Fields

        private Image img;
        private TextBlock textBlock;
        private RadioButton radioButton;

        #endregion

        #region Property

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public bool? IsChecked
        {
            get { return (bool)GetValue(IsCheckedProperty); }
            set { SetValue(IsCheckedProperty, value); }
        }

        public ImageSource ImgSource
        {
            get { return (ImageSource)GetValue(ImgSourceProperty); }
            set { SetValue(ImgSourceProperty, value); }
        }

        #region Dependency property

        public static readonly DependencyProperty IsCheckedProperty =
           DependencyProperty.Register("IsChecked", typeof(bool), typeof(AdminButton),
               new PropertyMetadata(false));

        public static readonly DependencyProperty TextProperty =
           DependencyProperty.Register("Text", typeof(string), typeof(AdminButton),
               new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty ImgSourceProperty =
           DependencyProperty.Register("ImgSource", typeof(ImageSource), typeof(AdminButton));

        #endregion

        #endregion

        #region Methods

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            if (this.Template != null)
            {
                img = this.Template.FindName("img", this) as Image;
                textBlock = this.Template.FindName("textBlock", this) as TextBlock;
                radioButton = this.Template.FindName("radioButton", this) as RadioButton;

                IsChecked = radioButton.IsChecked;
                img.Source = ImgSource;
                textBlock.Text = Text;
            }
        }

        #endregion 
    }
}
