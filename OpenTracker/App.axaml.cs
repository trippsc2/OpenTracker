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

        private static void MakeDefaultThemeFirst(IThemeSelector selector)
        {
            foreach (var theme in selector.Themes!)
            {
                if (theme.Name != "Default")
                {
                    continue;
                }
                
                selector.Themes.Remove(theme);
                selector.Themes.Insert(0, theme);
                break;
            }
        }

        private static IThemeSelector InitializeThemes(IComponentContext scope)
        {
            CopyDefaultThemesToAppData();
            var selector = scope.Resolve<IThemeSelector>();
            MakeDefaultThemeFirst(selector);

            return selector;
        }

        private static void SetThemeToLastOrDefault(IThemeSelector selector)
        {
            var lastThemeFilePath = AppPath.LastThemeFilePath;

            if (File.Exists(lastThemeFilePath))
            {
                selector.LoadSelectedTheme(lastThemeFilePath);
                return;
            }
            
            selector.ApplyTheme(selector.Themes![0]);
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

                using var scope = ContainerConfig.Configure(this).BeginLifetimeScope();
                var selector = InitializeThemes(scope);
                
                var saveLoadManager = scope.Resolve<ISaveLoadManager>();
                saveLoadManager.OpenSequenceBreaks(AppPath.SequenceBreakPath);
                desktop.MainWindow = new MainWindow()
                {
                    DataContext = scope.Resolve<IMainWindowVM>()
                };
                
                SetThemeToLastOrDefault(selector);

                desktop.Exit += (sender, e) =>
                {
                    saveLoadManager.SaveSequenceBreaks(AppPath.SequenceBreakPath);
                    selector.SaveSelectedTheme(AppPath.LastThemeFilePath);
                };
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}
