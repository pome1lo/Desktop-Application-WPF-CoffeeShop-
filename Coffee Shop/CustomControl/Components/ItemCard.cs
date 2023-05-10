using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace CustomControl
{
    [TemplatePart(Name = "imgBrush", Type = typeof(ImageBrush))]
    [TemplatePart(Name = "closeButton", Type = typeof(Button))]
    [TemplatePart(Name = "minusButton", Type = typeof(Button))]
    [TemplatePart(Name = "plusButton", Type = typeof(Button))]
    [TemplatePart(Name = "quanity", Type = typeof(TextBlock))]
    [TemplatePart(Name = "price", Type = typeof(TextBlock))]
    [TemplatePart(Name = "name", Type = typeof(TextBlock))]
    public class ItemCard : UserControl
    {
        #region Constructor

        static ItemCard()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ItemCard),
                new FrameworkPropertyMetadata(typeof(ItemCard)));
        }

        #endregion

        #region Property

        public Object Param
        {
            get { return (Object)GetValue(ParamProperty); }
            set { SetValue(ParamProperty, value); }
        }

        public ICommand CommandClose
        {
            get { return (ICommand)GetValue(CommandCloseProperty); }
            set { SetValue(CommandCloseProperty, value); }
        }

        public ICommand CommandMinus
        {
            get { return (ICommand)GetValue(CommandMinusProperty); }
            set { SetValue(CommandMinusProperty, value); }
        }

        public ICommand CommandPlus
        {
            get { return (ICommand)GetValue(CommandPlusProperty); }
            set { SetValue(CommandPlusProperty, value); }
        }

        public ushort Quanity
        {
            get { return (ushort)GetValue(QuanityProperty); }
            set { SetValue(QuanityProperty, value); }
        }
        public decimal Price
        {
            get { return (decimal)GetValue(PriceProperty); }
            set { SetValue(PriceProperty, value); }
        }
        public string ProdName
        {
            get { return (string)GetValue(ProdNameProperty); }
            set { SetValue(ProdNameProperty, value); }
        }

        public ImageSource Img
        {
            get { return (ImageSource)GetValue(ImgProperty); }
            set { SetValue(ImgProperty, value); }
        }

        #region DependencyProperty

        public static readonly DependencyProperty QuanityProperty =
            DependencyProperty.Register("Quanity", typeof(ushort), typeof(ItemCard),
                new PropertyMetadata(default));

        public static readonly DependencyProperty PriceProperty =
            DependencyProperty.Register("Price", typeof(decimal), typeof(ItemCard),
                new PropertyMetadata(default));

        public static readonly DependencyProperty ProdNameProperty =
            DependencyProperty.Register("ProdName", typeof(string), typeof(ItemCard),
                new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty ImgProperty =
            DependencyProperty.Register("Img", typeof(ImageSource), typeof(ItemCard),
                new PropertyMetadata(default));


        public static readonly DependencyProperty ParamProperty =
            DependencyProperty.Register("Param", typeof(Object), typeof(ItemCard),
                new PropertyMetadata(default));

        public static readonly DependencyProperty CommandPlusProperty =
          DependencyProperty.Register("CommandPlus", typeof(ICommand), typeof(ItemCard),
              new PropertyMetadata(default));



        public static readonly DependencyProperty CommandMinusProperty =
            DependencyProperty.Register("CommandMinus", typeof(ICommand), typeof(ItemCard),
                new PropertyMetadata(default));



        public static readonly DependencyProperty CommandCloseProperty =
            DependencyProperty.Register("CommandClose", typeof(ICommand), typeof(ItemCard),
                new PropertyMetadata(default));


        #endregion

        #endregion

        #region Fields

        private TextBlock quanity;
        private TextBlock price;
        private TextBlock name;
        private ImageBrush img;
        private Button minusButton;
        private Button plusButton;
        private Button closeButton;

        #endregion

        #region Methods

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (this.Template != null)
            {
                quanity = this.Template.FindName("quanity", this) as TextBlock;
                price = this.Template.FindName("price", this) as TextBlock;
                name = this.Template.FindName("name", this) as TextBlock;
                img = this.Template.FindName("imgBrush", this) as ImageBrush;

                minusButton = this.Template.FindName("minusButton", this) as Button;
                plusButton = this.Template.FindName("plusButton", this) as Button;
                closeButton = this.Template.FindName("closeButton", this) as Button;

                quanity.Text = Quanity.ToString();
                price.Text = "$ " + Price.ToString();
                name.Text = ProdName.ToString();
                img.ImageSource = Img;

                minusButton.Click += butClick;
                plusButton.Click += butClick;
                closeButton.Click += butClick;

                // навесить команду на крестик обновитьс писок м еню
            }
        }

        private void butClick(object sender, RoutedEventArgs e)
        {
            Param = ProdName;
        }

        #endregion
    }
}