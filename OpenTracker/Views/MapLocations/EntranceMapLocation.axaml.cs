using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using OpenTracker.ViewModels.MapLocations;

namespace OpenTracker.Views.MapLocations;

public sealed class EntranceMapLocation : ReactiveUserControl<EntranceMapLocationVM>
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