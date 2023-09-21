using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using OpenTracker.ViewModels.Menus;

namespace OpenTracker.Views.Menus;

public sealed class MenuHotkeyHeader : ReactiveUserControl<MenuHotkeyHeaderVM>
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