using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CustomControl
{
    public class WebView : UserControl
    {
        static WebView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(WebView),
                new FrameworkPropertyMetadata(typeof(WebView)));
        }
    }
}
