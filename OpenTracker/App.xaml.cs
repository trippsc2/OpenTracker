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
using System.Text.RegularExpressions;

namespace OpenTracker
{
    public class App : Application
    {
        public static IThemeSelector Selector { get; set; }

        public static string GetApplicationRoot()
        {
            string exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);

            Regex appPathMatcher = new Regex(@"(?<!fil)[A-Za-z]:\\+[\S\s]*?(?=\\+bin)");
            var appRoot = appPathMatcher.Match(exePath).Value;

            return appRoot;
        }

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

                string defaultThemesPath = GetApplicationRoot() +
                    Path.DirectorySeparatorChar + "Themes";

                foreach (string themeFile in Directory.GetFiles(defaultThemesPath))
                {
                    string themeFilename = Path.GetFileName(themeFile);

                    string newFilePath = opentrackerHomePath + Path.DirectorySeparatorChar +
                        "Themes" + Path.DirectorySeparatorChar + themeFilename;

                    if (File.Exists(newFilePath))
                        File.Delete(newFilePath);

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
