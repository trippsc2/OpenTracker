using System.Collections.Generic;
using System.Windows.Input;
using Avalonia.Controls;
using Avalonia.Layout;
using OpenTracker.Models.Requirements;

namespace OpenTracker.ViewModels.Menus
{
    /// <summary>
    /// This class contains the creation logic for menu item controls.
    /// </summary>
    public class MenuItemFactory : IMenuItemFactory
    {
        private readonly IMenuItemVM.Factory _itemFactory;

        private readonly DisplayAllLocationsRequirement.Factory _displayAllLocationsFactory;
        private readonly ShowItemCountsOnMapRequirement.Factory _showItemCountsOnMapFactory;
        private readonly DisplayMapsCompassesRequirement.Factory _displayMapsCompassesFactory;
        private readonly AlwaysDisplayDungeonItemsRequirement.Factory _alwaysDisplayDungeonItemsFactory;
        private readonly LayoutOrientationRequirement.Factory _layoutOrientationFactory;
        private readonly HorizontalUIPanelPlacementRequirement.Factory _horizontalUIPanelPlacementFactory;
        private readonly HorizontalItemsPanelPlacementRequirement.Factory _horizontalItemsPanelPlacementFactory;
        private readonly VerticalUIPanelPlacementRequirement.Factory _verticalUIPanelPlacementFactory;
        private readonly VerticalItemsPanelPlacementRequirement.Factory _verticalItemsPanelPlacementFactory;
        private readonly UIScaleRequirement.Factory _uiScaleFactory;

        public MenuItemFactory(
            IMenuItemVM.Factory itemFactory, DisplayAllLocationsRequirement.Factory displayAllLocationsFactory,
            ShowItemCountsOnMapRequirement.Factory showItemCountsOnMapFactory,
            DisplayMapsCompassesRequirement.Factory displayMapsCompassesFactory,
            AlwaysDisplayDungeonItemsRequirement.Factory alwaysDisplayDungeonItemsFactory,
            LayoutOrientationRequirement.Factory layoutOrientationFactory,
            HorizontalUIPanelPlacementRequirement.Factory horizontalUIPanelPlacementFactory,
            HorizontalItemsPanelPlacementRequirement.Factory horizontalItemsPanelPlacementFactory,
            VerticalUIPanelPlacementRequirement.Factory verticalUIPanelPlacementFactory,
            VerticalItemsPanelPlacementRequirement.Factory verticalItemsPanelPlacementFactory,
            UIScaleRequirement.Factory uiScaleFactory)
        {
            _itemFactory = itemFactory;

            _displayAllLocationsFactory = displayAllLocationsFactory;
            _showItemCountsOnMapFactory = showItemCountsOnMapFactory;
            _displayMapsCompassesFactory = displayMapsCompassesFactory;
            _alwaysDisplayDungeonItemsFactory = alwaysDisplayDungeonItemsFactory;
            _layoutOrientationFactory = layoutOrientationFactory;
            _horizontalUIPanelPlacementFactory = horizontalUIPanelPlacementFactory;
            _horizontalItemsPanelPlacementFactory = horizontalItemsPanelPlacementFactory;
            _verticalUIPanelPlacementFactory = verticalUIPanelPlacementFactory;
            _verticalItemsPanelPlacementFactory = verticalItemsPanelPlacementFactory;
            _uiScaleFactory = uiScaleFactory;
        }

        private List<IMenuItemVM> GetFileMenuItems(
            ICommand open, ICommand save, ICommand saveAs, ICommand reset, ICommand close)
        {
            return new List<IMenuItemVM>
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
            return new List<IMenuItemVM>
            {
                _itemFactory("Undo", hotkey: "Ctrl+Z", command: undo),
                _itemFactory("Redo", hotkey: "Ctrl+Y", command: redo),
                _itemFactory("-"),
                _itemFactory("Auto-Tracker...", command: autoTracker),
                _itemFactory("-"),
                _itemFactory("Sequence Breaks...", command: sequenceBreaks)
            };
        }

        private List<IMenuItemVM> GetViewMenuItems(
            ICommand toggleDisplayAllLocations, ICommand toggleShowItemCountsOnMap, ICommand toggleDisplayMapsCompasses,
            ICommand toggleAlwaysDisplayDungeonItems, ICommand colorSelect, ICommand changeLayoutOrientation,
            ICommand changeHorizontalUIPanelPlacement, ICommand changeHorizontalItemsPlacement,
            ICommand changeVerticalUIPanelPlacement, ICommand changeVerticalItemsPlacement,
            ICommand changeMapOrientation, ICommand changeUIScale, ICommand about)
        {
            return new List<IMenuItemVM>
            {
                _itemFactory("Theme"),
                _itemFactory("-"),
                _itemFactory("Display All Locations",
                    _displayAllLocationsFactory(true), "F11", toggleDisplayAllLocations),
                _itemFactory("Show Item Counts on Map",
                    _showItemCountsOnMapFactory(true),
                    command: toggleShowItemCountsOnMap),
                _itemFactory("-"),
                _itemFactory("Display Maps/Compass",
                    _displayMapsCompassesFactory(true),
                    command: toggleDisplayMapsCompasses),
                _itemFactory("Always Display Dungeon Items",
                    _alwaysDisplayDungeonItemsFactory(true),
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
                    _itemFactory("Dynamic"),
                    _itemFactory("Horizontal"),
                    _itemFactory("Vertical")
                }),
                _itemFactory("-"),
                _itemFactory("About...")
            };
        }

        public List<IMenuItemVM> GetMenuItems(
            ICommand open, ICommand save, ICommand saveAs, ICommand reset, ICommand close, ICommand undo, ICommand redo,
            ICommand autoTracker, ICommand sequenceBreaks, ICommand toggleDisplayAllLocations,
            ICommand toggleShowItemCountsOnMap, ICommand toggleDisplayMapsCompasses,
            ICommand toggleAlwaysDisplayDungeonItems, ICommand colorSelect, ICommand changeLayoutOrientation,
            ICommand changeHorizontalUIPanelPlacement, ICommand changeHorizontalItemsPlacement,
            ICommand changeVerticalUIPanelPlacement, ICommand changeVerticalItemsPlacement,
            ICommand changeMapOrientation, ICommand changeUIScale, ICommand about)
        {
            return new List<IMenuItemVM>
            {
                _itemFactory("File", items: GetFileMenuItems(open, save, saveAs, reset, close)),
                _itemFactory("Tracker", items: GetTrackerMenuItems(undo, redo, autoTracker, sequenceBreaks)),
                _itemFactory("Stream"),
                _itemFactory("View", items: GetViewMenuItems(
                    toggleDisplayAllLocations, toggleShowItemCountsOnMap, toggleDisplayMapsCompasses,
                    toggleAlwaysDisplayDungeonItems, colorSelect, changeLayoutOrientation,
                    changeHorizontalUIPanelPlacement, changeHorizontalItemsPlacement, changeVerticalUIPanelPlacement,
                    changeVerticalItemsPlacement, changeMapOrientation, changeUIScale, about))
            };
        }
    }
}