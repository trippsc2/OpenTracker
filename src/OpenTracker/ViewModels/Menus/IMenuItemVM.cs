using System.Collections.Generic;
using System.Windows.Input;
using OpenTracker.Models.Requirements;
using OpenTracker.Utils;
using ReactiveUI;

namespace OpenTracker.ViewModels.Menus;

public interface IMenuItemVM : IModelWrapper, IReactiveObject
{
    IMenuItemIconVM? Icon { get; }
    object Header { get; }
    ICommand? Command { get; }
    object? CommandParameter { get; }
    IList<IMenuItemVM> Items { get; }

    delegate IMenuItemVM Factory(
        string header, IRequirement? checkBoxRequirement = null, string? hotkey = null, ICommand? command = null,
        object? commandParameter = null, IList<IMenuItemVM>? items = null);
}