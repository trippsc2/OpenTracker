using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace OpenTracker.Views.Menus;

public class TopMenu : UserControl
{
    public TopMenu()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}