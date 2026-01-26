using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace OpenTracker.Views.Dungeons;

public partial class DungeonItem : UserControl
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