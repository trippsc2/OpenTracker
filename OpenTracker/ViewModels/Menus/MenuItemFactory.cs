using System.Collections.Generic;
using System.Windows.Input;
using OpenTracker.Models.SaveLoad;

namespace OpenTracker.ViewModels.Menus
{
    public class MenuItemFactory : IMenuItemFactory
    {
        private readonly IMenuItemVM.Factory _itemFactory;

        public MenuItemFactory(IMenuItemVM.Factory itemFactory)
        {
            _itemFactory = itemFactory;
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

        private List<IMenuItemVM> GetViewMenuItems()
        {
            return new List<IMenuItemVM>
            {
                _itemFactory("Theme"),
                _itemFactory("-"),
                _itemFactory("Display All Locations", hotkey: "F11"),
                _itemFactory("Show Item Counts on Map"),
                _itemFactory("-"),
                _itemFactory("Display Maps/Compass"),
                _itemFactory("Always Display Dungeon Items"),
                _itemFactory("-"),
                _itemFactory("Change Colors..."),
                _itemFactory("-"),
                _itemFactory("Layout Orientation", items: new List<IMenuItemVM>
                {
                    _itemFactory("Dynamic"),
                    _itemFactory("Horizontal"),
                    _itemFactory("Vertical")
                }),
                _itemFactory("Horizontal Orientation", items: new List<IMenuItemVM>
                {
                    _itemFactory("UI Panel Placement", items: new List<IMenuItemVM>
                    {
                        _itemFactory("Top"),
                        _itemFactory("Bottom")
                    }),
                    _itemFactory("Items Panel Placement", items: new List<IMenuItemVM>
                    {
                        _itemFactory("Left"),
                        _itemFactory("Right")
                    })
                }),
                _itemFactory("Vertical Orientation", items: new List<IMenuItemVM>
                {
                    _itemFactory("UI Panel Placement", items: new List<IMenuItemVM>
                    {
                        _itemFactory("Left"),
                        _itemFactory("Right")
                    }),
                    _itemFactory("Items Panel Placement", items: new List<IMenuItemVM>
                    {
                        _itemFactory("Top"),
                        _itemFactory("Bottom")
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
            ICommand changeMapOrientation, ICommand changeUIScale)
        {
            return new List<IMenuItemVM>
            {
                _itemFactory("File", items: GetFileMenuItems(open, save, saveAs, reset, close)),
                _itemFactory("Tracker", items: GetTrackerMenuItems(undo, redo, autoTracker, sequenceBreaks)),
                _itemFactory("Stream"),
                _itemFactory("View", items: GetViewMenuItems())
            };
        }
    }
}