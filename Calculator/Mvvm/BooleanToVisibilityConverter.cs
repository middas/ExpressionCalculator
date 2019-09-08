using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Calculator.Mvvm
{
    public class BooleanToVisibilityConverter : IValueConverter
    {
        public bool HiddenInsteadOfCollapsed { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Visibility visibility = Visibility.Visible;
            if (value is bool b)
            {
                visibility = b ? Visibility.Visible : HiddenInsteadOfCollapsed ? Visibility.Hidden : Visibility.Collapsed;
            }

            return visibility;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool result = false;
            if (value is Visibility v)
            {
                result = v == Visibility.Visible;
            }

            return result;
        }
    }
}