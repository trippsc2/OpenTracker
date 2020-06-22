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
using System.Reflection;

namespace OpenTracker
{
    public class App : Application
    {
        public static IThemeSelector Selector { get; set; }

        public static string GetApplicationRoot()
        {
            return Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
        }

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                string localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData,
                    Environment.SpecialFolderOption.Create);

                string openTrackerHomePath = Path.Combine(localAppData, "OpenTracker");

                string themePath = Path.Combine(openTrackerHomePath, "Themes");

                if (!Directory.Exists(themePath))
                {
                    Directory.CreateDirectory(themePath);
                }

                string defaultThemesPath = Path.Combine(GetApplicationRoot(), "Themes");

                foreach (string themeFile in Directory.GetFiles(defaultThemesPath))
                {
                    string themeFilename = Path.GetFileName(themeFile);

                    string newFilePath = Path.Combine(openTrackerHomePath, "Themes", themeFilename);

                    if (File.Exists(newFilePath))
                    {
                        File.Delete(newFilePath);
                    }

                    File.Copy(themeFile, newFilePath);
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

                string themeConfigPath = Path.Combine(openTrackerHomePath, "OpenTracker.theme");

                if (File.Exists(themeConfigPath))
                {
                    Selector.LoadSelectedTheme(themeConfigPath);
                }
                else
                {
                    Selector.ApplyTheme(Selector.Themes[0]);
                }

                desktop.Exit += (sender, e) => Selector.SaveSelectedTheme(themeConfigPath);

                dialogService.Owner = desktop.MainWindow;

                dialogService.Register<MessageBoxDialogVM, MessageBoxDialog>();
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}
