using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace OpenTracker.Views.Menus;

public partial class MenuHotkeyHeader : UserControl
{
    public MenuHotkeyHeader()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}