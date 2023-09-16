using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using OpenTracker.ViewModels.Maps;

namespace OpenTracker.Views.Maps;

public sealed class MapConnection : ReactiveUserControl<MapConnectionVM>
{
    public MapConnection()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}