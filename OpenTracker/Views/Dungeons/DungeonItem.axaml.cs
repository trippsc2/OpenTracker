using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using OpenTracker.ViewModels.Dungeons;

namespace OpenTracker.Views.Dungeons;

public sealed class DungeonItem : ReactiveUserControl<DungeonItemVM>
{
    public DungeonItem()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}