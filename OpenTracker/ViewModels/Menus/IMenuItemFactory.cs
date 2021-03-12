using System.Collections.Generic;
using System.Windows.Input;

namespace OpenTracker.ViewModels.Menus
{
    public interface IMenuItemFactory
    {
        List<IMenuItemVM> GetMenuItems(
            ICommand open, ICommand save, ICommand saveAs, ICommand reset, ICommand close, ICommand undo, ICommand redo,
            ICommand autoTracker, ICommand sequenceBreaks, ICommand toggleDisplayAllLocations,
            ICommand toggleShowItemCountsOnMap, ICommand toggleDisplayMapsCompasses,
            ICommand toggleAlwaysDisplayDungeonItems, ICommand colorSelect, ICommand changeLayoutOrientation,
            ICommand changeHorizontalUIPanelPlacement, ICommand changeHorizontalItemsPlacement,
            ICommand changeVerticalUIPanelPlacement, ICommand changeVerticalItemsPlacement,
            ICommand changeMapOrientation, ICommand changeUIScale);
    }
}