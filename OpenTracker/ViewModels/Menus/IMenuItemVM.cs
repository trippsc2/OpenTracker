using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using OpenTracker.Models.Requirements;

namespace OpenTracker.ViewModels.Menus
{
    public interface IMenuItemVM : INotifyPropertyChanged
    {
        IMenuItemIconVM? Icon { get; }
        object Header { get; }
        ICommand? Command { get; }
        object? CommandParameter { get; }
        IList<IMenuItemVM> Items { get; }

        delegate IMenuItemVM Factory(
            string header, IRequirement? checkBoxRequirement = null, string? hotkey = null, ICommand? command = null,
            object? commandParameter = null, List<IMenuItemVM>? items = null);
    }
}