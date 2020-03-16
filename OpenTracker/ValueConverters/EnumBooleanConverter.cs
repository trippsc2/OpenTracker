using Avalonia;
using Avalonia.Data.Converters;
using System;
using System.Globalization;

namespace OpenTracker.ValueConverters
{
    public class EnumBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string parameterString = parameter as string;

            if (parameterString == null)
                return AvaloniaProperty.UnsetValue;

            object parameterValue = Enum.Parse(value.GetType(), parameterString);

            return parameterValue.Equals(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string parameterString = parameter as string;

            if (parameterString == null)
                return AvaloniaProperty.UnsetValue;

            return Enum.Parse(targetType, parameterString);
        }
    }
}
