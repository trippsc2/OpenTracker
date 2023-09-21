using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using Avalonia;
using Avalonia.Data.Converters;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;

namespace OpenTracker.ValueConverters;

/// <summary>
/// This class contains logic to convert a string to a bitmap URI.
/// </summary>
public class BitmapValueConverter : IValueConverter
{
    /// <summary>
    /// Returns a bitmap URI from the specified string.
    /// </summary>
    /// <returns>
    /// A bitmap URI from the string.
    /// </returns>
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        switch (value)
        {
            case null:
                return null;
            case string @string when targetType == typeof(IImage):
            {
                var uri = new Uri(@string, UriKind.RelativeOrAbsolute);
                var scheme = uri.IsAbsoluteUri ? uri.Scheme : "file";

                switch (scheme)
                {
                    case "file":
                        return new Bitmap(@string);
                    default:
                        try
                        {
                            var assets = AvaloniaLocator.Current.GetService<IAssetLoader>();
                            return new Bitmap(assets?.Open(uri));
                        }
                        catch (FileNotFoundException ex)
                        {
                            Debug.WriteLine(ex.Message);
                            return null;
                        }
                }
            }
            default:
                throw new NotSupportedException();
        }
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new Exception("All bindings should be one-way.");
    }
}