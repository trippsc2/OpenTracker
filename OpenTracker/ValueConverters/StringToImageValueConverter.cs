using System;
using System.Diagnostics;
using System.IO;
using Avalonia;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using ReactiveUI;

namespace OpenTracker.ValueConverters;

public sealed class StringToImageValueConverter : IBindingTypeConverter
{
    public int GetAffinityForObjects(Type fromType, Type toType)
    {
        if (fromType == typeof(string) && toType == typeof(IImage))
        {
            return 100;
        }
        
        return 0;
    }

    public bool TryConvert(object? from, Type toType, object? conversionHint, out object? result)
    {
        if (from is not string imageSource)
        {
            result = null;
            return false;
        }
        
        var uri = new Uri(imageSource, UriKind.RelativeOrAbsolute);
        var scheme = uri.IsAbsoluteUri ? uri.Scheme : "file";

        if (scheme == "file")
        {
            result = new Bitmap(imageSource);
            return true;
        }
        
        try
        {
            var assets = AvaloniaLocator.Current.GetService<IAssetLoader>();
            result = new Bitmap(assets?.Open(uri));
            return true;
        }
        catch (FileNotFoundException ex)
        {
            Debug.WriteLine(ex.Message);
            result = null;
            return false;
        }
    }
}