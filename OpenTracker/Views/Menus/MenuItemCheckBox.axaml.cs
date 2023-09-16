using System.Reactive.Disposables;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using OpenTracker.ViewModels.Menus;
using ReactiveUI;

namespace OpenTracker.Views.Menus;

public sealed class MenuItemCheckBox : ReactiveUserControl<MenuItemCheckBoxVM>
{
    private CheckBox CheckBox => this.FindControl<CheckBox>("CheckBox");
    
    public MenuItemCheckBox()
    {
        InitializeComponent();
        this.WhenActivated(disposables =>
        {
            this.Bind(ViewModel,
                    vm => vm.Checked,
                    v => v.CheckBox.IsChecked)
                .DisposeWith(disposables);
        });
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}