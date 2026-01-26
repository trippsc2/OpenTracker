using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace OpenTracker.Views.ToolTips;

public partial class MapLocationToolTip : UserControl
{
    public MapLocationToolTip()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}