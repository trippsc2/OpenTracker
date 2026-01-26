using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace OpenTracker.Views.Maps;

public partial class Map : UserControl
{
    public Map()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}