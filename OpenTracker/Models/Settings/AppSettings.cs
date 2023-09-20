using System.Linq;
using System.Reflection;
using Avalonia.Media;
using OpenTracker.Models.SaveLoad;
using OpenTracker.Utils;
using OpenTracker.Utils.Autofac;
using Reactive.Bindings;

namespace OpenTracker.Models.Settings;

/// <summary>
/// This class contains app settings data.
/// </summary>
[DependencyInjection(SingleInstance = true)]
public sealed class AppSettings : IAppSettings
{
    public IBoundsSettings Bounds { get; }
    public ITrackerSettings Tracker { get; }
    public ILayoutSettings Layout { get; }
    public IColorSettings Colors { get; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="jsonConverter">
    ///     The JSON converter.
    /// </param>
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
        IJsonConverter jsonConverter, IBoundsSettings bounds, ITrackerSettings tracker, ILayoutSettings layout,
        IColorSettings colors)
    {
        Bounds = bounds;
        Tracker = tracker;
        Layout = layout;
        Colors = colors;

        var saveData = jsonConverter.Load<AppSettingsSaveData?>(AppPath.AppSettingsFilePath);

        if (saveData is null)
        {
            return;
        }
            
        Load(saveData);
    }

    /// <summary>
    /// Returns a new app settings save data instance for this item.
    /// </summary>
    /// <returns>
    /// A new app settings save data instance.
    /// </returns>
    public AppSettingsSaveData Save()
    {
        return new AppSettingsSaveData
        {
            Version = Assembly.GetExecutingAssembly().GetName().Version!,
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
            EmphasisFontColor = Colors.EmphasisFontColor.Value.ToString()?.ToLowerInvariant(),
            ConnectorColor = Colors.ConnectorColor.Value.ToString()?.ToLowerInvariant(),
            AccessibilityColors = Colors.AccessibilityColors
                .ToDictionary(
                    x => x.Key,
                    x => x.Value.Value.ToString()!.ToLowerInvariant())
        };
    }

    /// <summary>
    /// Loads app settings save data.
    /// </summary>
    public void Load(AppSettingsSaveData? saveData)
    {
        if (saveData is null)
        {
            return;
        }
            
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

        Layout.HorizontalUIPanelPlacement = saveData.HorizontalUIPanelPlacement;
        Layout.VerticalUIPanelPlacement = saveData.VerticalUIPanelPlacement;
        Layout.HorizontalItemsPlacement = saveData.HorizontalItemsPlacement;
        Layout.VerticalItemsPlacement = saveData.VerticalItemsPlacement;

        Layout.UIScale = saveData.UIScale == 0.0 ? 1.0 : saveData.UIScale;

        if (saveData.EmphasisFontColor != null)
        {
            Colors.EmphasisFontColor.Value = SolidColorBrush.Parse(saveData.EmphasisFontColor);
        }

        if (saveData.ConnectorColor != null)
        {
            Colors.ConnectorColor.Value = SolidColorBrush.Parse(saveData.ConnectorColor);
        }

        if (saveData.AccessibilityColors == null)
        {
            return;
        }

        foreach (var color in saveData.AccessibilityColors)
        {
            if (Colors.AccessibilityColors.TryGetValue(color.Key, out var accessibilityColor))
            {
                accessibilityColor.Value = SolidColorBrush.Parse(color.Value);
            }
            else
            {
                Colors.AccessibilityColors.Add(
                    color.Key,
                    new ReactiveProperty<SolidColorBrush> { Value = SolidColorBrush.Parse(color.Value) });
            }
        }
    }
}