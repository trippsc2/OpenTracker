using System.IO;
using Autofac;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Logging;
using Avalonia.Markup.Xaml;
using OpenTracker.Models;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Utils;
using OpenTracker.Utils.Themes;
using OpenTracker.ViewModels;
using OpenTracker.Views;

namespace OpenTracker
{
    public class App : Application
    {
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

        private static void SetThemeToLastOrDefault(IThemeManager selector)
        {
            var lastThemeFilePath = AppPath.LastThemeFilePath;

            if (!File.Exists(lastThemeFilePath))
            {
                return;
            }
            
            selector.LoadSelectedTheme(lastThemeFilePath);
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
                desktop.ShutdownMode = ShutdownMode.OnMainWindowClose;
                CopyDefaultThemesToAppData();

                using var scope = ContainerConfig.Configure().BeginLifetimeScope();

                var themeManagerFactory = scope.Resolve<IThemeManager.Factory>();
                var themeManager = themeManagerFactory(this, AppPath.AppDataThemesPath);
                var saveLoadManager = scope.Resolve<ISaveLoadManager>();
                saveLoadManager.OpenSequenceBreaks(AppPath.SequenceBreakPath);
                desktop.MainWindow = new MainWindow()
                {
                    DataContext = scope.Resolve<IMainWindowVM>()
                };
                
                SetThemeToLastOrDefault(themeManager);

                desktop.Exit += (_, _) =>
                {
                    saveLoadManager.SaveSequenceBreaks(AppPath.SequenceBreakPath);
                    themeManager.SaveSelectedTheme(AppPath.LastThemeFilePath);
                };
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}
