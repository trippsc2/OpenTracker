using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
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
using OpenTracker.Utils.Themes;

namespace OpenTracker.ViewModels.Menus
{
    /// <summary>
    /// This class contains the creation logic for menu item controls.
    /// </summary>
    public class MenuItemFactory : IMenuItemFactory
    {
        private readonly ICaptureWindowMenuCollection _captureWindowMenuItems;
        private readonly IThemeManager _themeManager;
        
        private readonly IAlwaysDisplayDungeonItemsRequirementDictionary _alwaysDisplayDungeonItemsRequirements;
        private readonly IDisplayMapsCompassesRequirementDictionary _displayMapsCompassesRequirements;

        private readonly IMenuItemVM.Factory _itemFactory;

        private readonly IThemeSelectedRequirement.Factory _themeSelectedFactory;
        private readonly IDisplayAllLocationsRequirement.Factory _displayAllLocationsFactory;
        private readonly IShowItemCountsOnMapRequirement.Factory _showItemCountsOnMapFactory;
        private readonly ILayoutOrientationRequirement.Factory _layoutOrientationFactory;
        private readonly IHorizontalUIPanelPlacementRequirement.Factory _horizontalUIPanelPlacementFactory;
        private readonly IHorizontalItemsPanelPlacementRequirement.Factory _horizontalItemsPanelPlacementFactory;
        private readonly IVerticalUIPanelPlacementRequirement.Factory _verticalUIPanelPlacementFactory;
        private readonly IVerticalItemsPanelPlacementRequirement.Factory _verticalItemsPanelPlacementFactory;
        private readonly IMapOrientationRequirement.Factory _mapOrientationFactory;
        private readonly IUIScaleRequirement.Factory _uiScaleFactory;

        public MenuItemFactory(
            ICaptureWindowMenuCollection captureWindowMenuItems, IThemeManager themeManager,
            IAlwaysDisplayDungeonItemsRequirementDictionary alwaysDisplayDungeonItemsRequirements,
            IDisplayMapsCompassesRequirementDictionary displayMapsCompassesRequirements,
            IMenuItemVM.Factory itemFactory, IThemeSelectedRequirement.Factory themeSelectedFactory,
            IDisplayAllLocationsRequirement.Factory displayAllLocationsFactory,
            IShowItemCountsOnMapRequirement.Factory showItemCountsOnMapFactory,
            ILayoutOrientationRequirement.Factory layoutOrientationFactory,
            IHorizontalUIPanelPlacementRequirement.Factory horizontalUIPanelPlacementFactory,
            IHorizontalItemsPanelPlacementRequirement.Factory horizontalItemsPanelPlacementFactory,
            IVerticalUIPanelPlacementRequirement.Factory verticalUIPanelPlacementFactory,
            IVerticalItemsPanelPlacementRequirement.Factory verticalItemsPanelPlacementFactory,
            IMapOrientationRequirement.Factory mapOrientationFactory, IUIScaleRequirement.Factory uiScaleFactory)
        {
            _themeManager = themeManager;
            _captureWindowMenuItems = captureWindowMenuItems;
            
            _itemFactory = itemFactory;

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
        }

        private List<IMenuItemVM> GetFileMenuItems(
            ICommand open, ICommand save, ICommand saveAs, ICommand reset, ICommand close)
        {
            return new()
            {
                _itemFactory("Open...", hotkey: "Ctrl+O", command: open),
                _itemFactory("Save...", hotkey: "Ctrl+S", command: save),
                _itemFactory("Save As...", hotkey: "Ctrl+Shift+S", command: saveAs),
                _itemFactory("Reset...", hotkey: "F5", command: reset),
                _itemFactory("-"),
                _itemFactory("Close", hotkey: "Alt+F4", command: close)
            };
        }

        private List<IMenuItemVM> GetTrackerMenuItems(
            ICommand undo, ICommand redo, ICommand autoTracker, ICommand sequenceBreaks)
        {
            return new()
            {
                _itemFactory("Undo", hotkey: "Ctrl+Z", command: undo),
                _itemFactory("Redo", hotkey: "Ctrl+Y", command: redo),
                _itemFactory("-"),
                _itemFactory("Auto-Tracker...", command: autoTracker),
                _itemFactory("-"),
                _itemFactory("Sequence Breaks...", command: sequenceBreaks)
            };
        }

        private List<IMenuItemVM> GetCaptureMenuItems(
            ICaptureWindowMenuCollection captureWindowMenuItems, ICommand captureDesign)
        {
            return new()
            {
                _itemFactory("Windows", items: captureWindowMenuItems),
                _itemFactory("-"),
                _itemFactory("Design Capture Windows...", command: captureDesign)
            };
        }

        private List<IMenuItemVM> GetThemeMenuItems(ICommand changeTheme)
        {
            return _themeManager.Themes.Select(theme => _itemFactory(
                theme.Name, _themeSelectedFactory(theme), command: changeTheme,
                commandParameter: theme)).ToList();
        }

        private List<IMenuItemVM> GetViewMenuItems(
            ICommand changeTheme, ICommand toggleDisplayAllLocations, ICommand toggleShowItemCountsOnMap,
            ICommand toggleDisplayMapsCompasses, ICommand toggleAlwaysDisplayDungeonItems, ICommand colorSelect,
            ICommand changeLayoutOrientation, ICommand changeHorizontalUIPanelPlacement,
            ICommand changeHorizontalItemsPlacement, ICommand changeVerticalUIPanelPlacement,
            ICommand changeVerticalItemsPlacement, ICommand changeMapOrientation, ICommand changeUIScale,
            ICommand about)
        {
            return new()
            {
                _itemFactory("Theme", items: GetThemeMenuItems(changeTheme)),
                _itemFactory("-"),
                _itemFactory("Display All Locations",
                    _displayAllLocationsFactory(true), "F11", toggleDisplayAllLocations),
                _itemFactory("Show Item Counts on Map",
                    _showItemCountsOnMapFactory(true),
                    command: toggleShowItemCountsOnMap),
                _itemFactory("-"),
                _itemFactory("Display Maps/Compass",
                    _displayMapsCompassesRequirements[true],
                    command: toggleDisplayMapsCompasses),
                _itemFactory("Always Display Dungeon Items",
                    _alwaysDisplayDungeonItemsRequirements[true],
                    command: toggleAlwaysDisplayDungeonItems),
                _itemFactory("-"),
                _itemFactory("Change Colors...", command: colorSelect),
                _itemFactory("-"),
                _itemFactory("Layout Orientation", items: new List<IMenuItemVM>
                {
                    _itemFactory("Dynamic", _layoutOrientationFactory(null),
                        command: changeLayoutOrientation, commandParameter: null),
                    _itemFactory("Horizontal",
                        _layoutOrientationFactory(Orientation.Horizontal),
                        command: changeLayoutOrientation, commandParameter: Orientation.Horizontal),
                    _itemFactory("Vertical",
                        _layoutOrientationFactory(Orientation.Vertical),
                        command: changeLayoutOrientation, commandParameter: Orientation.Vertical),
                }),
                _itemFactory("Horizontal Orientation", items: new List<IMenuItemVM>
                {
                    _itemFactory("UI Panel Placement", items: new List<IMenuItemVM>
                    {
                        _itemFactory("Top",
          _horizontalUIPanelPlacementFactory(Dock.Top),
                            command: changeHorizontalUIPanelPlacement, commandParameter: Dock.Top),
                        _itemFactory("Bottom",
                            _horizontalUIPanelPlacementFactory(Dock.Bottom),
                            command: changeHorizontalUIPanelPlacement, commandParameter: Dock.Bottom)
                    }),
                    _itemFactory("Items Panel Placement", items: new List<IMenuItemVM>
                    {
                        _itemFactory("Left",
                            _horizontalItemsPanelPlacementFactory(Dock.Left),
                            command: changeHorizontalItemsPlacement, commandParameter: Dock.Left),
                        _itemFactory("Right",
                            _horizontalItemsPanelPlacementFactory(Dock.Right),
                            command: changeHorizontalItemsPlacement, commandParameter: Dock.Right)
                    })
                }),
                _itemFactory("Vertical Orientation", items: new List<IMenuItemVM>
                {
                    _itemFactory("UI Panel Placement", items: new List<IMenuItemVM>
                    {
                        _itemFactory("Left",
                            _verticalUIPanelPlacementFactory(Dock.Left),
                            command: changeVerticalUIPanelPlacement, commandParameter: Dock.Left),
                        _itemFactory("Right",
                            _verticalUIPanelPlacementFactory(Dock.Right),
                            command: changeVerticalUIPanelPlacement, commandParameter: Dock.Right)
                    }),
                    _itemFactory("Items Panel Placement", items: new List<IMenuItemVM>
                    {
                        _itemFactory("Top",
                            _verticalItemsPanelPlacementFactory(Dock.Top),
                            command: changeVerticalItemsPlacement, commandParameter: Dock.Top),
                        _itemFactory("Bottom",
                            _verticalItemsPanelPlacementFactory(Dock.Bottom),
                            command: changeVerticalItemsPlacement, commandParameter: Dock.Bottom)
                    })
                }),
                _itemFactory("Map Orientation", items: new List<IMenuItemVM>
                {
                    _itemFactory("Dynamic",
                        _mapOrientationFactory(null),
                        command: changeMapOrientation, commandParameter: null),
                    _itemFactory("Horizontal",
                        _mapOrientationFactory(Orientation.Horizontal),
                        command: changeMapOrientation, commandParameter: Orientation.Horizontal),
                    _itemFactory("Vertical",
                        _mapOrientationFactory(Orientation.Vertical),
                        command: changeMapOrientation, commandParameter: Orientation.Vertical)
                }),
                _itemFactory("UI Scale", items: new List<IMenuItemVM>
                {
                    _itemFactory("100%", _uiScaleFactory(1.0),
                        command: changeUIScale, commandParameter: 1.0),
                    _itemFactory("125%", _uiScaleFactory(1.25),
                        command: changeUIScale, commandParameter: 1.25),
                    _itemFactory("150%", _uiScaleFactory(1.5),
                        command: changeUIScale, commandParameter: 1.5),
                    _itemFactory("175%", _uiScaleFactory(1.75),
                        command: changeUIScale, commandParameter: 1.75),
                    _itemFactory("200%", _uiScaleFactory(2.0),
                        command: changeUIScale, commandParameter: 2.0)
                }),
                _itemFactory("-"),
                _itemFactory("About...", command: about)
            };
        }

        public List<IMenuItemVM> GetMenuItems(
            ICommand open, ICommand save, ICommand saveAs, ICommand reset, ICommand close, ICommand undo, ICommand redo,
            ICommand autoTracker, ICommand sequenceBreaks, ICommand captureDesign, ICommand changeTheme,
            ICommand toggleDisplayAllLocations, ICommand toggleShowItemCountsOnMap, ICommand toggleDisplayMapsCompasses,
            ICommand toggleAlwaysDisplayDungeonItems, ICommand colorSelect, ICommand changeLayoutOrientation,
            ICommand changeHorizontalUIPanelPlacement, ICommand changeHorizontalItemsPlacement,
            ICommand changeVerticalUIPanelPlacement, ICommand changeVerticalItemsPlacement,
            ICommand changeMapOrientation, ICommand changeUIScale, ICommand about)
        {
            return new()
            {
                _itemFactory("File", items: GetFileMenuItems(open, save, saveAs, reset, close)),
                _itemFactory("Tracker", items: GetTrackerMenuItems(undo, redo, autoTracker, sequenceBreaks)),
                //_itemFactory("Capture", items: GetCaptureMenuItems(_captureWindowMenuItems, captureDesign)),
                _itemFactory("View", items: GetViewMenuItems(
                    changeTheme, toggleDisplayAllLocations, toggleShowItemCountsOnMap, toggleDisplayMapsCompasses,
                    toggleAlwaysDisplayDungeonItems, colorSelect, changeLayoutOrientation,
                    changeHorizontalUIPanelPlacement, changeHorizontalItemsPlacement, changeVerticalUIPanelPlacement,
                    changeVerticalItemsPlacement, changeMapOrientation, changeUIScale, about))
            };
        }
    }
}