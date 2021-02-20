using OpenTracker.Models.AccessibilityLevels;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Models.Utils;
using OpenTracker.Utils;
using System.Collections.Generic;
using System.Reflection;

namespace OpenTracker.Models.Settings
{
    /// <summary>
    /// This is the class for application data to be saved to file.
    /// </summary>
    public class AppSettings : Singleton<AppSettings>, IAppSettings
    {
        public IBoundsSettings Bounds { get; }
        public ITrackerSettings Tracker { get; }
        public ILayoutSettings Layout { get; }
        public IColorSettings Colors { get; }

        public AppSettings()
            : this(new BoundsSettings(), new TrackerSettings(), new LayoutSettings(),
                  new ColorSettings())
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bounds">
        /// The bounds settings.
        /// </param>
        /// <param name="tracker">
        /// The tracker settings.
        /// </param>
        /// <param name="layout">
        /// The layout settings.
        /// </param>
        /// <param name="colors">
        /// The color settings.
        /// </param>
        public AppSettings(
            IBoundsSettings bounds, ITrackerSettings tracker, ILayoutSettings layout,
            IColorSettings colors)
        {
            Bounds = bounds;
            Tracker = tracker;
            Layout = layout;
            Colors = colors;

            var saveData = JsonConversion.Load<AppSettingsSaveData>(AppPath.AppSettingsFilePath);

            if (saveData != null)
            {
                Load(saveData);
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
                DisplayMapsCompasses = Layout.DisplayMapsCompasses,
                AlwaysDisplayDungeonItems = Layout.AlwaysDisplayDungeonItems,
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
            Bounds.Maximized = saveData.Maximized;
            Bounds.X = saveData.X;
            Bounds.Y = saveData.Y;
            Bounds.Width = saveData.Width;
            Bounds.Height = saveData.Height;
            Tracker.DisplayAllLocations = saveData.DisplayAllLocations;
            Tracker.ShowItemCountsOnMap = saveData.ShowItemCountsOnMap;

            if (saveData.DisplayMapsCompasses.HasValue)
            {
                Layout.DisplayMapsCompasses = saveData.DisplayMapsCompasses.Value;
            }

            if (saveData.AlwaysDisplayDungeonItems.HasValue)
            {
                Layout.AlwaysDisplayDungeonItems = saveData.AlwaysDisplayDungeonItems.Value;
            }

            Layout.LayoutOrientation = saveData.LayoutOrientation;
            Layout.MapOrientation = saveData.MapOrientation;

            // Code to handle change of type from 1.3.2 to later versions.
            if (saveData.Version == null)
            {
                AppSettingsConversion.ConvertPre132(saveData);
            }

            Layout.HorizontalUIPanelPlacement = saveData.HorizontalUIPanelPlacement;
            Layout.VerticalUIPanelPlacement = saveData.VerticalUIPanelPlacement;
            Layout.HorizontalItemsPlacement = saveData.HorizontalItemsPlacement;
            Layout.VerticalItemsPlacement = saveData.VerticalItemsPlacement;

            Layout.UIScale = saveData.UIScale == 0.0 ? 1.0 : saveData.UIScale;

            if (saveData.EmphasisFontColor != null)
            {
                Colors.EmphasisFontColor = saveData.EmphasisFontColor;
            }

            if (saveData.ConnectorColor != null)
            {
                Colors.ConnectorColor = saveData.ConnectorColor;
            }

            if (saveData.AccessibilityColors == null)
            {
                return;
            }

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
