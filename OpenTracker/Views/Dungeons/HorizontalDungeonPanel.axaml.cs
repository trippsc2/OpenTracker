using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using OpenTracker.ViewModels.Dungeons;

namespace OpenTracker.Views.Dungeons;

public sealed class HorizontalDungeonPanel : ReactiveUserControl<HorizontalDungeonPanelVM>
{
    public HorizontalDungeonPanel()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}