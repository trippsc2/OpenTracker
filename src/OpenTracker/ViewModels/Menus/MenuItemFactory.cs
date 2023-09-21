using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Layout;
using OpenTracker.Models.Requirements.AlwaysDisplayDungeonItems;
using OpenTracker.Models.Requirements.DisplayAllLocations;
using OpenTracker.Models.Requirements.DisplaysMapsCompasses;
using OpenTracker.Models.Requirements.ItemsPanelPlacement;
using OpenTracker.Models.Requirements.LayoutOrientation;
using OpenTracker.Models.Requirements.MapOrientation;
using OpenTracker.Models.Requirements.ShowItemCountsOnMap;
using OpenTracker.Models.Requirements.ThemeSelected;
using OpenTracker.Models.Requirements.UIPanelPlacement;
using OpenTracker.Models.Requirements.UIScale;
using OpenTracker.Utils.Autofac;
using OpenTracker.Utils.Themes;

namespace OpenTracker.ViewModels.Menus;

/// <summary>
/// This class contains the creation logic for menu item controls.
/// </summary>
[DependencyInjection(SingleInstance = true)]
public sealed class MenuItemFactory : IMenuItemFactory
{
    private readonly IThemeManager _themeManager;
        
    private readonly IAlwaysDisplayDungeonItemsRequirementDictionary _alwaysDisplayDungeonItemsRequirements;
    private readonly IDisplayMapsCompassesRequirementDictionary _displayMapsCompassesRequirements;

    private readonly ThemeSelectedRequirement.Factory _themeSelectedFactory;
    private readonly DisplayAllLocationsRequirement.Factory _displayAllLocationsFactory;
    private readonly ShowItemCountsOnMapRequirement.Factory _showItemCountsOnMapFactory;
    private readonly LayoutOrientationRequirement.Factory _layoutOrientationFactory;
    private readonly HorizontalUIPanelPlacementRequirement.Factory _horizontalUIPanelPlacementFactory;
    private readonly HorizontalItemsPanelPlacementRequirement.Factory _horizontalItemsPanelPlacementFactory;
    private readonly VerticalUIPanelPlacementRequirement.Factory _verticalUIPanelPlacementFactory;
    private readonly VerticalItemsPanelPlacementRequirement.Factory _verticalItemsPanelPlacementFactory;
    private readonly MapOrientationRequirement.Factory _mapOrientationFactory;
    private readonly UIScaleRequirement.Factory _uiScaleFactory;

    private readonly MenuItemVM _separator;

    public MenuItemFactory(
        IThemeManager themeManager,
        IAlwaysDisplayDungeonItemsRequirementDictionary alwaysDisplayDungeonItemsRequirements,
        IDisplayMapsCompassesRequirementDictionary displayMapsCompassesRequirements,
        ThemeSelectedRequirement.Factory themeSelectedFactory,
        DisplayAllLocationsRequirement.Factory displayAllLocationsFactory,
        ShowItemCountsOnMapRequirement.Factory showItemCountsOnMapFactory,
        LayoutOrientationRequirement.Factory layoutOrientationFactory,
        HorizontalUIPanelPlacementRequirement.Factory horizontalUIPanelPlacementFactory,
        HorizontalItemsPanelPlacementRequirement.Factory horizontalItemsPanelPlacementFactory,
        VerticalUIPanelPlacementRequirement.Factory verticalUIPanelPlacementFactory,
        VerticalItemsPanelPlacementRequirement.Factory verticalItemsPanelPlacementFactory,
        MapOrientationRequirement.Factory mapOrientationFactory,
        UIScaleRequirement.Factory uiScaleFactory)
    {
        _themeManager = themeManager;

        _themeSelectedFactory = themeSelectedFactory;
        _displayAllLocationsFactory = displayAllLocationsFactory;
        _showItemCountsOnMapFactory = showItemCountsOnMapFactory;
        _layoutOrientationFactory = layoutOrientationFactory;
        _horizontalUIPanelPlacementFactory = horizontalUIPanelPlacementFactory;
        _horizontalItemsPanelPlacementFactory = horizontalItemsPanelPlacementFactory;
        _verticalUIPanelPlacementFactory = verticalUIPanelPlacementFactory;
        _verticalItemsPanelPlacementFactory = verticalItemsPanelPlacementFactory;
        _mapOrientationFactory = mapOrientationFactory;
        _uiScaleFactory = uiScaleFactory;
        _alwaysDisplayDungeonItemsRequirements = alwaysDisplayDungeonItemsRequirements;
        _displayMapsCompassesRequirements = displayMapsCompassesRequirements;
        
        _separator = new MenuItemVM("-");
    }

    public List<MenuItemVM> GetMenuItems()
    {
        return new List<MenuItemVM>
        {
            new("File", items: GetFileMenuItems()),
            new("Tracker", items: GetTrackerMenuItems()),
            new("View", items: GetViewMenuItems())
        };
    }

    private List<MenuItemVM> GetFileMenuItems()
    {
        return new List<MenuItemVM>
        {
            new("Open...", hotkey: "Ctrl+O"),
            new("Save...", hotkey: "Ctrl+S"),
            new("Save As...", hotkey: "Ctrl+Shift+S"),
            new("Reset...", hotkey: "F5"),
            _separator,
            new("Close", hotkey: "Alt+F4")
        };
    }

    private List<MenuItemVM> GetTrackerMenuItems()
    {
        return new List<MenuItemVM>
        {
            new("Undo", hotkey: "Ctrl+Z"),
            new("Redo", hotkey: "Ctrl+Y"),
            _separator,
            new("Auto-Tracker..."),
            _separator,
            new("Sequence Breaks...")
        };
    }

    private List<MenuItemVM> GetViewMenuItems()
    {
        return new List<MenuItemVM>
        {
            new("Theme", items: GetThemeMenuItems()),
            _separator,
            new("Display All Locations", _displayAllLocationsFactory(true), "F11"),
            new("Show Item Counts on Map", _showItemCountsOnMapFactory(true)),
            _separator,
            new("Display Maps/Compass", _displayMapsCompassesRequirements[true]),
            new("Always Display Dungeon Items", _alwaysDisplayDungeonItemsRequirements[true]),
            _separator,
            new("Change Colors..."),
            _separator,
            new("Layout Orientation", items: GetLayoutOrientationMenuItems()),
            new("Horizontal Orientation", items: GetHorizontalOrientationMenuItems()),
            new("Vertical Orientation", items: GetVerticalOrientationMenuItems()),
            new("Map Orientation", items: GetMapOrientationMenuItems()),
            new("UI Scale", items: GetUIScaleMenuItems()),
            _separator,
            new("About...")
        };
    }
    
    private List<MenuItemVM> GetThemeMenuItems()
    {
        return _themeManager.Themes
            .Select(theme =>
                new MenuItemVM(theme.Name, _themeSelectedFactory(theme), commandParameter: theme))
            .ToList();
    }

    private List<MenuItemVM> GetLayoutOrientationMenuItems()
    {
        return new List<MenuItemVM>
        {
            new("Dynamic", _layoutOrientationFactory(null), commandParameter: null),
            new("Horizontal", _layoutOrientationFactory(Orientation.Horizontal), commandParameter: Orientation.Horizontal),
            new("Vertical", _layoutOrientationFactory(Orientation.Vertical), commandParameter: Orientation.Vertical)
        };
    }

    private List<MenuItemVM> GetHorizontalOrientationMenuItems()
    {
        return new List<MenuItemVM>
        {
            new("UI Panel Placement", items: GetHorizontalUIPanelPlacementMenuItems()),
            new("Items Panel Placement", items: GetHorizontalItemsPanelPlacementMenuItems())
        };
    }
    
    private List<MenuItemVM> GetHorizontalUIPanelPlacementMenuItems()
    {
        return new List<MenuItemVM>
        {
            new("Top", _horizontalUIPanelPlacementFactory(Dock.Top), commandParameter: Dock.Top),
            new("Bottom", _horizontalUIPanelPlacementFactory(Dock.Bottom), commandParameter: Dock.Bottom)
        };
    }
    
    private List<MenuItemVM> GetHorizontalItemsPanelPlacementMenuItems()
    {
        return new List<MenuItemVM>
        {
            new("Left", _horizontalItemsPanelPlacementFactory(Dock.Left), commandParameter: Dock.Left),
            new("Right", _horizontalItemsPanelPlacementFactory(Dock.Right), commandParameter: Dock.Right)
        };
    }

    private List<MenuItemVM> GetVerticalOrientationMenuItems()
    {
        return new List<MenuItemVM>
        {
            new("UI Panel Placement", items: GetVerticalUIPanelPlacementMenuItems()),
            new("Items Panel Placement", items: GetVerticalItemsPanelPlacementMenuItems())
        };
    }
    
    private List<MenuItemVM> GetVerticalUIPanelPlacementMenuItems()
    {
        return new List<MenuItemVM>
        {
            new("Left", _verticalUIPanelPlacementFactory(Dock.Left), commandParameter: Dock.Left),
            new("Right", _verticalUIPanelPlacementFactory(Dock.Right), commandParameter: Dock.Right)
        };
    }
    
    private List<MenuItemVM> GetVerticalItemsPanelPlacementMenuItems()
    {
        return new List<MenuItemVM>
        {
            new("Top", _verticalItemsPanelPlacementFactory(Dock.Top), commandParameter: Dock.Top),
            new("Bottom", _verticalItemsPanelPlacementFactory(Dock.Bottom), commandParameter: Dock.Bottom)
        };
    }
    
    private List<MenuItemVM> GetMapOrientationMenuItems()
    {
        return new List<MenuItemVM>
        {
            new("Dynamic", _mapOrientationFactory(null), commandParameter: null),
            new("Horizontal", _mapOrientationFactory(Orientation.Horizontal), commandParameter: Orientation.Horizontal),
            new("Vertical", _mapOrientationFactory(Orientation.Vertical), commandParameter: Orientation.Vertical)
        };
    }

    private List<MenuItemVM> GetUIScaleMenuItems()
    {
        return new List<MenuItemVM>
        {
            new("100%", _uiScaleFactory(1.0), commandParameter: 1.0),
            new("125%", _uiScaleFactory(1.25), commandParameter: 1.25),
            new("150%", _uiScaleFactory(1.5), commandParameter: 1.5),
            new("175%", _uiScaleFactory(1.75), commandParameter: 1.75),
            new("200%", _uiScaleFactory(2.0), commandParameter: 2.0)
        };
    }
}