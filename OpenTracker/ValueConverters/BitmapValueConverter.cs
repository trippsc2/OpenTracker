using Avalonia;
using Avalonia.Data.Converters;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;

namespace OpenTracker.ValueConverters
{
    public class BitmapValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            if (value is string && targetType == typeof(IBitmap))
            {
                var uri = new Uri((string)value, UriKind.RelativeOrAbsolute);
                var scheme = uri.IsAbsoluteUri ? uri.Scheme : "file";

                switch (scheme)
                {
                    case "file":
                        return new Bitmap((string)value);

                    default:
                        try
                        {
                            var assets = AvaloniaLocator.Current.GetService<IAssetLoader>();
                            return new Bitmap(assets.Open(uri));
                        }
                        catch (FileNotFoundException ex)
                        {
                            Debug.WriteLine(ex.Message);
                            return null;
                        }
                }
            }

            throw new NotSupportedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
