using System;
using System.Globalization;
using System.Windows.Data;

namespace Dotnetstore.WPF.Intranet.Converters;

public class CultureInfoTextEnglishNameNativeNameConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is null)
        {
            return "";
        }

        var item = (CultureInfo)value;

        if (item is null)
        {
            return "";
        }

        return $"{item.EnglishName} ({item.NativeName})";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}