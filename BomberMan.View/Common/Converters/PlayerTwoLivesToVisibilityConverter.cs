using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace BomberMan.View.Common.Converters
{
    public class PlayerTwoLivesToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => System.Convert.ToInt32(value) >= 2  ? Visibility.Visible : Visibility.Hidden;
       
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => Binding.DoNothing;
    }
}
