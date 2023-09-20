using System.Collections.Generic;

namespace OpenTracker.ViewModels.Menus;

public interface IMenuItemFactory
{
    List<MenuItemVM> GetMenuItems();
}