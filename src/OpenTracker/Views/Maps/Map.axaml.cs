using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using OpenTracker.ViewModels.Maps;

namespace OpenTracker.Views.Maps;

public sealed class Map : ReactiveUserControl<MapVM>
{
    public Map()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}