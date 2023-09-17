using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using OpenTracker.ViewModels.MapLocations;

namespace OpenTracker.Views.MapLocations;

public sealed class TakeAnyMapLocation : ReactiveUserControl<TakeAnyMapLocationVM>
{
    public TakeAnyMapLocation()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}