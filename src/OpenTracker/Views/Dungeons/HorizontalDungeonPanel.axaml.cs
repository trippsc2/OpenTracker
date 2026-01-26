using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace OpenTracker.Views.Dungeons;

public partial class HorizontalDungeonPanel : UserControl
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