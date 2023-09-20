using System.Collections.Generic;
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
    public TopMenu()
    {
        InitializeComponent();

        this.WhenActivated(disposables =>
        {
            if (ViewModel is null)
            {
                return;
            }

            ViewModel!.OpenFileDialogInteraction
                .RegisterHandler(async interaction =>
                {
                    interaction.SetOutput(await OpenFileDialogAsync().ConfigureAwait(true));
                })
                .DisposeWith(disposables);

            ViewModel!.SaveFileDialogInteraction
                .RegisterHandler(async interaction =>
                {
                    interaction.SetOutput(await SaveFileDialogAsync().ConfigureAwait(true));
                })
                .DisposeWith(disposables);
            
            ViewModel!.OpenErrorBoxInteraction
                .RegisterHandler(async interaction =>
                {
                    await OpenErrorBoxAsync(interaction.Input).ConfigureAwait(true);
                    interaction.SetOutput(Unit.Default);
                })
                .DisposeWith(disposables);
            
            ViewModel!.OpenMessageBoxInteraction
                .RegisterHandler(async interaction =>
                {
                    interaction.SetOutput(await OpenMessageBoxAsync(interaction.Input).ConfigureAwait(true));
                })
                .DisposeWith(disposables);
            
            ViewModel!.OpenDialogInteraction
                .RegisterHandler(interaction =>
                {
                    OpenDialogAsync(interaction.Input);
                    interaction.SetOutput(Unit.Default);
                })
                .DisposeWith(disposables);

            ViewModel!.RequestCloseInteraction
                .RegisterHandler(interaction =>
                {
                    interaction.SetOutput(Unit.Default);
                    var mainWindow = GetMainWindow();
                    
                    if (mainWindow is null)
                    { 
                        return;
                    }
                    
                    mainWindow.Close();
                })
                .DisposeWith(disposables);
        });
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private static async Task<string?> OpenFileDialogAsync()
    {
        var mainWindow = GetMainWindow();

        if (mainWindow is null)
        {
            return null;
        }
        
        var dialog = new OpenFileDialog
        {
            AllowMultiple = false,
            Filters = new List<FileDialogFilter> { new() { Name = "JSON", Extensions = new List<string> {"json"} } }
        };

        var result = await dialog.ShowAsync(mainWindow).ConfigureAwait(true);
        
        if (result is null || result.Length == 0)
        {
            return null;
        }

        return result[0];
    }

    private static async Task<string?> SaveFileDialogAsync()
    {
        var mainWindow = GetMainWindow();

        if (mainWindow is null)
        {
            return null;
        }
        
        var dialog = new SaveFileDialog
        {
            Filters = new List<FileDialogFilter> { new() { Name = "JSON", Extensions = new List<string> {"json"} } }
        };
        
        return await dialog.ShowAsync(mainWindow).ConfigureAwait(true);
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