using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace OpenTracker.Views.Dungeons;

public partial class DungeonPanel : UserControl
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