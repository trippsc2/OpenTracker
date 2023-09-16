using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using OpenTracker.ViewModels.ToolTips;

namespace OpenTracker.Views.ToolTips;

public sealed class MapLocationToolTipMarking : ReactiveUserControl<MapLocationToolTipMarkingVM>
{
    public MapLocationToolTipMarking()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}