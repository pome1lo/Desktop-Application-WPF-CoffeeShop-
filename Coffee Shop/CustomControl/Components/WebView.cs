using System.Windows;
using System.Windows.Controls;

namespace CustomControl
{
    public class WebView : UserControl
    {
        #region Constructor

        static WebView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(WebView),
                new FrameworkPropertyMetadata(typeof(WebView)));
        }

        #endregion
    }
}
