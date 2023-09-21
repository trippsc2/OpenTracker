using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using OpenTracker.ViewModels.Menus;

namespace OpenTracker.Views.Menus;

public sealed class MenuItemCheckBox : ReactiveUserControl<MenuItemCheckBoxVM>
{
    public MenuItemCheckBox()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}