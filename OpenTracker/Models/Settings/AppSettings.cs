using Avalonia.Controls;
using Newtonsoft.Json;
using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.SaveLoad;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace OpenTracker.Models.Settings
{
    /// <summary>
    /// This is the class for application data to be saved to file.
    /// </summary>
    public class AppSettings
    {
        private static readonly object _syncLock = new object();
        private static volatile AppSettings _instance;

        public static AppSettings Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_syncLock)
                    {
                        if (_instance == null)
                        {
                            _instance = new AppSettings();
                        }
                    }
                }

                return _instance;
            }
        }

        public BoundsSettings Bounds { get; } =
            new BoundsSettings();
        public TrackerSettings Tracker { get; } =
            new TrackerSettings();
        public LayoutSettings Layout { get; } =
            new LayoutSettings();
        public ColorSettings Colors { get; } =
            new ColorSettings();

        /// <summary>
        /// Constructor
        /// </summary>
        public AppSettings()
        {
            string appSettingsPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "OpenTracker", "OpenTracker.json");
            
            if (File.Exists(appSettingsPath))
            {
                string jsonContent = File.ReadAllText(appSettingsPath);
                AppSettingsSaveData saveData =
                    JsonConvert.DeserializeObject<AppSettingsSaveData>(jsonContent);
                Load(saveData);
            }
            else
            {
                Colors.AccessibilityColors.Add(AccessibilityLevel.None, "#ffff3030");
                Colors.AccessibilityColors.Add(AccessibilityLevel.Partial, "#ffff8c00");
                Colors.AccessibilityColors.Add(AccessibilityLevel.Inspect, "#ff6495ed");
                Colors.AccessibilityColors.Add(AccessibilityLevel.SequenceBreak, "#ffffff00");
                Colors.AccessibilityColors.Add(AccessibilityLevel.Normal, "#ff00ff00");
                Colors.AccessibilityColors.Add(AccessibilityLevel.Cleared, "#ff333333");
            }
        }

        /// <summary>
        /// Returns a new app settings save data instance for this item.
        /// </summary>
        /// <returns>
        /// A new app settings save data instance.
        /// </returns>
        public AppSettingsSaveData Save()
        {
            return new AppSettingsSaveData()
            {
                Version = Assembly.GetExecutingAssembly().GetName().Version,
                Maximized = Bounds.Maximized,
                X = Bounds.X,
                Y = Bounds.Y,
                Width = Bounds.Width,
                Height = Bounds.Height,
                DisplayAllLocations = Tracker.DisplayAllLocations,
                ShowItemCountsOnMap = Tracker.ShowItemCountsOnMap,
                LayoutOrientation = Layout.LayoutOrientation,
                MapOrientation = Layout.MapOrientation,
                HorizontalUIPanelPlacement = Layout.HorizontalUIPanelPlacement,
                VerticalUIPanelPlacement = Layout.VerticalUIPanelPlacement,
                HorizontalItemsPlacement = Layout.HorizontalItemsPlacement,
                VerticalItemsPlacement = Layout.VerticalItemsPlacement,
                UIScale = Layout.UIScale,
                EmphasisFontColor = Colors.EmphasisFontColor,
                ConnectorColor = Colors.ConnectorColor,
                AccessibilityColors =
                    new Dictionary<AccessibilityLevel, string>(Colors.AccessibilityColors)
            };
        }

        /// <summary>
        /// Loads app settings save data.
        /// </summary>
        public void Load(AppSettingsSaveData saveData)
        {
            if (saveData == null)
            {
                throw new ArgumentNullException(nameof(saveData));
            }

            Bounds.Maximized = saveData.Maximized;
            Bounds.X = saveData.X;
            Bounds.Y = saveData.Y;
            Bounds.Width = saveData.Width;
            Bounds.Height = saveData.Height;
            Tracker.DisplayAllLocations = saveData.DisplayAllLocations;
            Tracker.ShowItemCountsOnMap = saveData.ShowItemCountsOnMap;
            Layout.LayoutOrientation = saveData.LayoutOrientation;
            Layout.MapOrientation = saveData.MapOrientation;

            // Code to handle change of type from 1.3.2 to later versions.
            if (saveData.Version == null)
            {
                Layout.HorizontalUIPanelPlacement = saveData.HorizontalUIPanelPlacement switch
                {
                    Dock.Bottom => Dock.Top,
                    _ => Dock.Bottom
                };

                Layout.VerticalUIPanelPlacement = saveData.VerticalUIPanelPlacement switch
                {
                    Dock.Bottom => Dock.Left,
                    _ => Dock.Right
                };

                Layout.HorizontalItemsPlacement = saveData.HorizontalItemsPlacement switch
                {
                    Dock.Bottom => Dock.Left,
                    _ => Dock.Right
                };

                Layout.VerticalItemsPlacement = saveData.VerticalItemsPlacement switch
                {
                    Dock.Bottom => Dock.Top,
                    _ => Dock.Bottom
                };
            }
            else
            {
                Layout.HorizontalUIPanelPlacement = saveData.HorizontalUIPanelPlacement;
                Layout.VerticalUIPanelPlacement = saveData.VerticalUIPanelPlacement;
                Layout.HorizontalItemsPlacement = saveData.HorizontalItemsPlacement;
                Layout.VerticalItemsPlacement = saveData.VerticalItemsPlacement;
            }

            Layout.UIScale = saveData.UIScale == 0.0 ? 1.0 : saveData.UIScale;
            Colors.EmphasisFontColor = saveData.EmphasisFontColor;
            Colors.ConnectorColor = saveData.ConnectorColor;

            foreach (var color in saveData.AccessibilityColors)
            {
                if (Colors.AccessibilityColors.ContainsKey(color.Key))
                {
                    Colors.AccessibilityColors[color.Key] = color.Value;
                }
                else
                {
                    Colors.AccessibilityColors.Add(color);
                }
            }
        }
    }
}
