using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.ThemeManager;
using OpenTracker.Interfaces;
using OpenTracker.Utils;
using OpenTracker.ViewModels;
using OpenTracker.Views;
using System.IO;

namespace OpenTracker
{
    public class App : Application
    {
        public static IThemeSelector Selector { get; set; }

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                Selector = ThemeSelector.Create("Themes");
                
                // Make sure that first value in Themes collection is the "Default" theme
                foreach (ITheme theme in Selector.Themes)
                {
                    if (theme.Name == "Default")
                    {
                        Selector.Themes.Remove(theme);
                        Selector.Themes.Insert(0, theme);
                        break;
                    }
                }

                IDialogService dialogService = new DialogService();

                desktop.MainWindow = new MainWindow()
                {
                    DataContext = new MainWindowVM(dialogService),
                    Selector = Selector
                };

                if (File.Exists("OpenTracker.theme"))
                    Selector.LoadSelectedTheme("OpenTracker.theme");
                else
                    Selector.ApplyTheme(Selector.Themes[0]);

                desktop.Exit += (sender, e) => Selector.SaveSelectedTheme("OpenTracker.theme");

                dialogService.Owner = desktop.MainWindow;

                dialogService.Register<MessageBoxDialogVM, MessageBoxDialog>();
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}
