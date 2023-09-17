using System.Reactive;
using System.Reactive.Disposables;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using OpenTracker.Utils;
using OpenTracker.ViewModels.Dialogs;
using OpenTracker.ViewModels.Menus;
using OpenTracker.Views.Dialogs;
using ReactiveUI;
using Splat;

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

            if (ViewModel is null)
            {
                return;
            }
            
            ViewModel!.OpenErrorBoxInteraction.RegisterHandler(async interaction =>
            {
                await OpenErrorBoxAsync(interaction.Input).ConfigureAwait(true);
                interaction.SetOutput(Unit.Default);
            });
            
            ViewModel!.OpenMessageBoxInteraction.RegisterHandler(async interaction =>
            {
                interaction.SetOutput(await OpenMessageBoxAsync(interaction.Input).ConfigureAwait(true));
            });
            
            ViewModel!.OpenDialogInteraction.RegisterHandler(interaction =>
            {
                OpenDialogAsync(interaction.Input);
                interaction.SetOutput(Unit.Default);
            });
        });
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private static async Task OpenErrorBoxAsync(ErrorBoxDialogVM viewModel)
    {
        var mainWindow = GetMainWindow();
        var view = Locator.Current.GetService<IViewFor<ErrorBoxDialogVM>>() as ErrorBoxDialog;
        
        view!.ViewModel = viewModel;
        
        await view.ShowDialog(mainWindow);
    }
    
    private static async Task<bool> OpenMessageBoxAsync(MessageBoxDialogVM viewModel)
    {
        var mainWindow = GetMainWindow();
        var view = Locator.Current.GetService<IViewFor<MessageBoxDialogVM>>() as MessageBoxDialog;
        
        view!.ViewModel = viewModel;
        
        return await view.ShowDialog<bool>(mainWindow).ConfigureAwait(true);
    }

    private static void OpenDialogAsync(ViewModel viewModel)
    {
        var mainWindow = GetMainWindow();
        var view = Locator.Current.GetService(viewModel.GetViewType()) as Window;
        
        view!.DataContext = viewModel;

        view.Show(mainWindow);
    }

    private static Window? GetMainWindow()
    {
        var lifetime = Application.Current?.ApplicationLifetime;
        
        return lifetime is IClassicDesktopStyleApplicationLifetime desktopLifetime
            ? desktopLifetime.MainWindow
            : null;
    }
}