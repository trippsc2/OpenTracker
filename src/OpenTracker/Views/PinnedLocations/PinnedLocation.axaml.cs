using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace OpenTracker.Views.PinnedLocations;

public partial class PinnedLocation : UserControl
{
    public PinnedLocation()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}