using System.Collections.Generic;
using System.Windows.Input;
using OpenTracker.Models.Requirements;
using OpenTracker.Utils;

namespace OpenTracker.ViewModels.Menus
{
    public class MenuItemVM : ViewModelBase, IMenuItemVM
    {
        // Allows for IModelWrapper to be implemented.
        public object Model { get; } = new object();
        
        public IMenuItemIconVM? Icon { get; }
        public object Header { get; }
        public ICommand? Command { get; }
        public object? CommandParameter { get; }
        public IList<IMenuItemVM> Items { get; }

        public MenuItemVM(
            MenuItemCheckBoxVM.Factory checkBoxFactory, IMenuHotkeyHeaderVM.Factory hotkeyFactory, string header,
            IRequirement? checkBoxRequirement = null, string? hotkey = null, ICommand? command = null,
            object? commandParameter = null, List<IMenuItemVM>? items = null)
        {
            if (hotkey is null)
            {
                Header = header;
            }
            else
            {
                Header = hotkeyFactory(hotkey, header);
            }

            Command = command;
            CommandParameter = commandParameter;
            Items = items ?? new List<IMenuItemVM>();

            if (checkBoxRequirement is null)
            {
                return;
            }

            Icon = checkBoxFactory(checkBoxRequirement);
        }
    }
}