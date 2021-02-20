using System;
using System.IO;
using System.Reflection;

namespace OpenTracker.Utils
{
    /// <summary>
    /// This class contains properties for returning relevant file/folder paths.
    /// </summary>
    public static class AppPath
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
        public static string AvaloniaLogFilePath
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
        public static string AppDataThemesPath
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
        public static string AppRootPath
        {
            get
            {
                if (_appRootPath == null)
                {
                    var assembly = Assembly.GetEntryAssembly() ??
                        throw new NullReferenceException();

                    _appRootPath = Path.GetDirectoryName(assembly.Location) ??
                        throw new NullReferenceException();
                }

                return _appRootPath;
            }
        }

        private static string? _appRootThemesPath;
        public static string AppRootThemesPath
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
        public static string LastThemeFilePath
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

        private static string? _appSettingsFilePath;
        public static string AppSettingsFilePath
        {
            get
            {
                if (_appSettingsFilePath == null)
                {
                    _appSettingsFilePath = Path.Combine(AppDataPath, "OpenTracker.json");
                }

                return _appSettingsFilePath;
            }
        }

        private static string? _sequenceBreakPath;
        public static string SequenceBreakPath
        {
            get
            {
                if (_sequenceBreakPath == null)
                {
                    _sequenceBreakPath = Path.Combine(AppDataPath, "sequencebreak.json");
                }

                return _sequenceBreakPath;
            }
        }
    }
}
