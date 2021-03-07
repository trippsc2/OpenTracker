using Avalonia.Controls;
using Avalonia.Layout;
using OpenTracker.Models.AccessibilityLevels;
using System;
using System.Collections.Generic;

namespace OpenTracker.Models.SaveLoad
{
    public class AppSettingsSaveData
    {
        public Version? Version { get; set; }
        public bool? Maximized { get; set; }
        public double? X { get; set; }
        public double? Y { get; set; }
        public double? Width { get; set; }
        public double? Height { get; set; }
        public bool DisplayAllLocations { get; set; }
        public bool ShowItemCountsOnMap { get; set; }
        public bool? DisplayMapsCompasses { get; set; }
        public bool? AlwaysDisplayDungeonItems { get; set; }
        public Orientation? LayoutOrientation { get; set; }
        public Orientation? MapOrientation { get; set; }
        public Dock HorizontalUIPanelPlacement { get; set; }
        public Dock VerticalUIPanelPlacement { get; set; }
        public Dock HorizontalItemsPlacement { get; set; }
        public Dock VerticalItemsPlacement { get; set; }
        public double UIScale { get; set; }
        public string? EmphasisFontColor { get; set; }
        public string? ConnectorColor { get; set; }
        public Dictionary<AccessibilityLevel, string>? AccessibilityColors { get; set; }
    }
}
