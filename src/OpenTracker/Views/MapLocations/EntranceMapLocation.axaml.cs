using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace OpenTracker.Views.MapLocations;

public partial class EntranceMapLocation : UserControl
{
    public EntranceMapLocation()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}