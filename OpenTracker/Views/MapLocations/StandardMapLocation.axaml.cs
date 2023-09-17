using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using OpenTracker.ViewModels.MapLocations;

namespace OpenTracker.Views.MapLocations;

public sealed class StandardMapLocation : ReactiveUserControl<StandardMapLocationVM>
{
    public StandardMapLocation()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}