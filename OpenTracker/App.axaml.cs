using Autofac;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Logging;
using Avalonia.Markup.Xaml;
using Avalonia.ThemeManager;
using OpenTracker.Models;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Utils;
using OpenTracker.ViewModels;
using OpenTracker.Views;
using System.IO;

namespace OpenTracker
{
    public class App : Application
    {
        public static IThemeSelector? Selector { get; private set; }

        private static void CopyDefaultThemesToAppData()
        {
            var themePath = AppPath.AppDataThemesPath;

            if (!Directory.Exists(themePath))
            {
                Directory.CreateDirectory(themePath);
            }

            foreach (var srcTheme in Directory.GetFiles(AppPath.AppRootThemesPath))
            {
                var filename = Path.GetFileName(srcTheme);
                var destTheme = Path.Combine(themePath, filename);

                if (File.Exists(destTheme))
                {
                    File.Delete(destTheme);
                }

                File.Copy(srcTheme, destTheme);
            }
        }

        private static void MakeDefaultThemeFirst()
        {
            foreach (var theme in Selector!.Themes!)
            {
                if (theme.Name != "Default")
                {
                    continue;
                }
                
                Selector.Themes.Remove(theme);
                Selector.Themes.Insert(0, theme);
                break;
            }
        }

        private void InitializeThemes()
        {
            CopyDefaultThemesToAppData();
            Selector = ThemeSelector.Create(AppPath.AppDataThemesPath, this);
            MakeDefaultThemeFirst();
        }

        private static void SetThemeToLastOrDefault()
        {
            var lastThemeFilePath = AppPath.LastThemeFilePath;

            if (File.Exists(lastThemeFilePath))
            {
                Selector!.LoadSelectedTheme(lastThemeFilePath);
            }
            else
            {
                Selector!.ApplyTheme(Selector!.Themes![0]);
            }
        }

        public override void Initialize()
        {
            Logger.Sink = new AvaloniaSerilogSink(
                AppPath.AvaloniaLogFilePath, Serilog.Events.LogEventLevel.Warning);

            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                InitializeThemes();

                desktop.ShutdownMode = ShutdownMode.OnMainWindowClose;

                using var scope = ContainerConfig.Configure().BeginLifetimeScope();
                
                var saveLoadManager = scope.Resolve<ISaveLoadManager>();
                saveLoadManager.OpenSequenceBreaks(AppPath.SequenceBreakPath);
                desktop.MainWindow = new MainWindow()
                {
                    DataContext = scope.Resolve<IMainWindowVM>()
                };
                
                SetThemeToLastOrDefault();

                desktop.Exit += (sender, e) =>
                {
                    saveLoadManager.SaveSequenceBreaks(AppPath.SequenceBreakPath);
                    Selector!.SaveSelectedTheme(AppPath.LastThemeFilePath);
                };
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}
