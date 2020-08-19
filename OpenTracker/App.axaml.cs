using Avalonia;
using Avalonia.Controls;
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
        public static IThemeSelector Selector { get; private set; }
        public static IDialogService DialogService { get; private set; }

        private static string GetAppRootFolder()
        {
            return Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
        }

        private static string GetAppRootThemesFolder()
        {
            return Path.Combine(GetAppRootFolder(), "Themes");
        }

        private static string GetAppDataFolder()
        {
            string localAppData = Environment.GetFolderPath(
                Environment.SpecialFolder.LocalApplicationData,
                Environment.SpecialFolderOption.Create);

            return Path.Combine(localAppData, "OpenTracker");
        }

        private static string GetAppDataThemesFolder()
        {
            return Path.Combine(GetAppDataFolder(), "Themes");
        }

        private static void CopyDefaultThemesToAppData()
        {
            string themePath = GetAppDataThemesFolder();

            if (!Directory.Exists(themePath))
            {
                Directory.CreateDirectory(themePath);
            }

            foreach (var themeFile in Directory.GetFiles(GetAppRootThemesFolder()))
            {
                string themeFilename = Path.GetFileName(themeFile);
                string newThemeFile = Path.Combine(themePath, themeFilename);

                if (File.Exists(newThemeFile))
                {
                    File.Delete(newThemeFile);
                }

                File.Copy(themeFile, newThemeFile);
            }
        }

        private static void MakeDefaultThemeFirst()
        {
            foreach (ITheme theme in Selector.Themes)
            {
                if (theme.Name == "Default")
                {
                    Selector.Themes.Remove(theme);
                    Selector.Themes.Insert(0, theme);
                    break;
                }
            }
        }

        private static void InitializeThemes()
        {
            CopyDefaultThemesToAppData();
            Selector = ThemeSelector.Create(GetAppDataThemesFolder());
            MakeDefaultThemeFirst();
        }

        private static string GetThemeConfigFile()
        {
            return Path.Combine(GetAppDataFolder(), "OpenTracker.theme");
        }

        private static void SetThemeToLastOrDefault()
        {
            string themeConfigFile = GetThemeConfigFile();

            if (File.Exists(themeConfigFile))
            {
                Selector.LoadSelectedTheme(themeConfigFile);
            }
            else
            {
                Selector.ApplyTheme(Selector.Themes[0]);
            }
        }

        private static void InitializeDialogService(Window owner)
        {
            if (owner == null)
            {
                throw new ArgumentNullException(nameof(owner));
            }

            DialogService = new DialogService(owner);
            DialogService.Register<MessageBoxDialogVM, MessageBoxDialog>();
        }

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                InitializeThemes();

                desktop.MainWindow = new MainWindow()
                {
                    DataContext = new MainWindowVM()
                };
                
                SetThemeToLastOrDefault();
                InitializeDialogService(desktop.MainWindow);

                desktop.Exit += (sender, e) => Selector.SaveSelectedTheme(GetThemeConfigFile());
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}
