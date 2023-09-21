using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using OpenTracker.ViewModels.Areas;

namespace OpenTracker.Views.Areas;

public sealed class MapArea : ReactiveUserControl<MapAreaVM>
{
    public MapArea()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}