using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace BomberMan.View.Common.Converters
{
    public class PlayerOneLiveToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => System.Convert.ToInt32(value) >= 1 ? Visibility.Visible : Visibility.Hidden;
        
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => Binding.DoNothing;
    }
}
