using System.Reactive.Disposables;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using OpenTracker.ViewModels.Menus;
using ReactiveUI;

namespace OpenTracker.Views.Menus;

public sealed class TopMenu : ReactiveUserControl<TopMenuVM>
{
    private Menu Menu => this.FindControl<Menu>("Menu");
    
    public TopMenu()
    {
        InitializeComponent();

        this.WhenActivated(disposables =>
        {
            this.OneWayBind(ViewModel,
                    vm => vm.Items,
                    v => v.Menu.Items)
                .DisposeWith(disposables);
        });
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}