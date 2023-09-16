using System.Reactive.Disposables;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using OpenTracker.ViewModels.Menus;
using ReactiveUI;

namespace OpenTracker.Views.Menus;

public sealed class MenuHotkeyHeader : ReactiveUserControl<MenuHotkeyHeaderVM>
{
    private TextBlock Hotkey => this.FindControl<TextBlock>("Hotkey");
    private TextBlock Header => this.FindControl<TextBlock>("Header");
    
    public MenuHotkeyHeader()
    {
        InitializeComponent();
        this.WhenActivated(disposables =>
        {
            this.OneWayBind(ViewModel,
                    vm => vm.Hotkey,
                    v => v.Hotkey.Text)
                .DisposeWith(disposables);
            this.OneWayBind(ViewModel,
                    vm => vm.Header,
                    v => v.Header.Text)
                .DisposeWith(disposables);
        });
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}