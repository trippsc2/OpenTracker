using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using OpenTracker.ViewModels.Dungeons;

namespace OpenTracker.Views.Dungeons;

public sealed class DungeonPanel : ReactiveUserControl<DungeonPanelVM>
{
    public DungeonPanel()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}