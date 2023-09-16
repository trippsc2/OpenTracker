using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using OpenTracker.ViewModels.MapLocations;

namespace OpenTracker.Views.MapLocations;

public sealed class MapLocationMarking : ReactiveUserControl<MapLocationMarkingVM>
{
    public MapLocationMarking()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}