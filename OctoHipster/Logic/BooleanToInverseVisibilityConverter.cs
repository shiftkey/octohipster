using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace OctoHipster.Logic
{
    public class BooleanToInverseVisibilityConverter : ConverterMarkupExtension<BooleanToInverseVisibilityConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is bool && (bool)value ? Visibility.Collapsed : Visibility.Visible;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
