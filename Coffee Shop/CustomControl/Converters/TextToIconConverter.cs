using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace CustomControl.Converters
{
    public class TextToIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var content = value?.ToString();
            if (string.IsNullOrEmpty(content))
            {
                return value;
            }
            switch(Enum.Parse(typeof(TextBoxType), content))
            {
                case TextBoxType.Clear:
                    return new Uri(@"pack://application:,,,/CustomControl;component/Images/BrowseFile.png");
                case TextBoxType.BrowseFile: 
                    return new Uri(@"pack://application:,,,/CustomControl;component/Images/folderExplore.png");
                case TextBoxType.BrowseFolder:
                    return new Uri(@"pack://application:,,,/CustomControl;component/Images/clear.png");
            }
            return new Uri(@"pack://application:,,,/CustomControl;component/Images/clear.png");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
