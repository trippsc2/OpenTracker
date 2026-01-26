using System;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Layout;
using OpenTracker.Models.Accessibility;

namespace OpenTracker.Models.SaveLoad;

public class AppSettingsSaveData
{
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public Version? Version { get; init; }
    public bool? Maximized { get; init; }
    public double? X { get; init; }
    public double? Y { get; init; }
    public double? Width { get; init; }
    public double? Height { get; init; }
    public bool DisplayAllLocations { get; init; }
    public bool ShowItemCountsOnMap { get; init; }
    public bool? DisplayMapsCompasses { get; init; }
    public bool? AlwaysDisplayDungeonItems { get; init; }
    public Orientation? LayoutOrientation { get; init; }
    public Orientation? MapOrientation { get; init; }
    public Dock HorizontalUIPanelPlacement { get; init; }
    public Dock VerticalUIPanelPlacement { get; init; }
    public Dock HorizontalItemsPlacement { get; init; }
    public Dock VerticalItemsPlacement { get; init; }
    public double UIScale { get; init; }
    public string? EmphasisFontColor { get; init; }
    public string? ConnectorColor { get; init; }
    public Dictionary<AccessibilityLevel, string>? AccessibilityColors { get; init; }
}