using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using OpenTracker.ViewModels.Dungeons;

namespace OpenTracker.Views.Dungeons;

public sealed class VerticalDungeonPanel : ReactiveUserControl<VerticalDungeonPanelVM>
{
    public VerticalDungeonPanel()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}