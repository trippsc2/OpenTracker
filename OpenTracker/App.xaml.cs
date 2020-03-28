using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using OpenTracker.Interfaces;
using OpenTracker.Utils;
using OpenTracker.ViewModels;
using OpenTracker.Views;

namespace OpenTracker
{
    public class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                IDialogService dialogService = new DialogService();

                desktop.MainWindow = new MainWindow
                {
                    DataContext = new MainWindowVM(dialogService),
                };

                dialogService.Owner = desktop.MainWindow;

                dialogService.Register<MessageBoxDialogVM, MessageBoxDialog>();
                dialogService.Register<AppSettingsVM, ColorSelectDialog>();
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}
