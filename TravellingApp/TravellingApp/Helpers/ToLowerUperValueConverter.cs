using System;
using System.Globalization;
using Xamarin.Forms;

namespace SysDatecScanApp.Helpers
{
    public class ToLowerUperValueConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (parameter)
            {
                case "U":
                    var str = value as string;
                    return string.IsNullOrEmpty(str) ? string.Empty : str.ToUpper();
                    break;
                case "L":
                    var str2 = value as string;
                    return string.IsNullOrEmpty(str2) ? string.Empty : str2.ToLower();
                    break;
                default:
                    return value;
                    break;
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
