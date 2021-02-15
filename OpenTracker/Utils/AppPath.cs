using System;
using System.IO;
using System.Reflection;

namespace OpenTracker.Utils
{
    /// <summary>
    /// This class contains methods for returning relevant file/folder paths.
    /// </summary>
    internal static class AppPath
    {
        private static string? _appDataPath;
        private static string AppDataPath
        {
            get
            {
                if (_appDataPath == null)
                {
                    var appDataPath = Environment.GetFolderPath(
                        Environment.SpecialFolder.LocalApplicationData,
                        Environment.SpecialFolderOption.Create);

                    _appDataPath = Path.Combine(appDataPath, "OpenTracker");
                }

                return _appDataPath;
            }
        }

        private static string? _avaloniaLogFilePath;
        internal static string AvaloniaLogFilePath
        {
            get
            {
                if (_avaloniaLogFilePath == null)
                {
                    _avaloniaLogFilePath = Path.Combine(AppDataPath, "OpenTracker.Avalonia.log");
                }

                return _avaloniaLogFilePath;
            }
        }

        private static string? _appDataThemesPath;
        internal static string AppDataThemesPath
        {
            get
            {
                if (_appDataThemesPath == null)
                {
                    _appDataThemesPath = Path.Combine(AppDataPath, "Themes");
                }

                return _appDataThemesPath;
            }
        }

        private static string? _appRootPath;
        private static string AppRootPath
        {
            get
            {
                if (_appRootPath == null)
                {
                    _appRootPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
                }

                return _appRootPath;
            }
        }

        private static string? _appRootThemesPath;
        internal static string AppRootThemesPath
        {
            get
            {
                if (_appRootThemesPath == null)
                {
                    _appRootThemesPath = Path.Combine(AppRootPath, "Themes");
                }

                return _appRootThemesPath;
            }
        }

        private static string? _lastThemeFilePath;
        internal static string LastThemeFilePath
        {
            get
            {
                if (_lastThemeFilePath == null)
                {
                    _lastThemeFilePath = Path.Combine(AppDataPath, "OpenTracker.theme");
                }

                return _lastThemeFilePath;
            }
        }
    }
}
