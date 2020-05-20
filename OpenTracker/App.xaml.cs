using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.ThemeManager;
using OpenTracker.Interfaces;
using OpenTracker.Utils;
using OpenTracker.ViewModels;
using OpenTracker.Views;
using System;
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
                string themePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) +
                    Path.DirectorySeparatorChar + "OpenTracker" +
                    Path.DirectorySeparatorChar + "Themes";

                if (!Directory.Exists(themePath))
                    Directory.CreateDirectory(themePath);

                string opentrackerHomePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) +
                   Path.DirectorySeparatorChar + "OpenTracker";

                foreach (string file in Directory.GetFiles("Themes"))
                {
                    string newFilePath = opentrackerHomePath + Path.DirectorySeparatorChar + file;

                    if (File.Exists(newFilePath))
                        File.Delete(newFilePath);

                    File.Copy(file, newFilePath);
                }

                Selector = ThemeSelector.Create(themePath);
                
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

                string themeConfigPath = opentrackerHomePath + Path.DirectorySeparatorChar + "OpenTracker.theme";

                if (File.Exists(themeConfigPath))
                    Selector.LoadSelectedTheme(themeConfigPath);
                else
                    Selector.ApplyTheme(Selector.Themes[0]);

                desktop.Exit += (sender, e) => Selector.SaveSelectedTheme(themeConfigPath);

                dialogService.Owner = desktop.MainWindow;

                dialogService.Register<MessageBoxDialogVM, MessageBoxDialog>();
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}
