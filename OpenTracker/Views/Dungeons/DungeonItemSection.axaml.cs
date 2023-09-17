using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using OpenTracker.ViewModels.Dungeons;

namespace OpenTracker.Views.Dungeons;

public sealed class DungeonItemSection : ReactiveUserControl<DungeonItemSectionVM>
{
    public DungeonItemSection()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}