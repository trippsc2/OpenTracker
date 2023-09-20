using System.Collections.Generic;
using System.Windows.Input;
using OpenTracker.Models.Requirements;
using OpenTracker.Utils;
using ReactiveUI.Fody.Helpers;

namespace OpenTracker.ViewModels.Menus;

public sealed class MenuItemVM : ViewModel
{
    public IMenuItemIconVM? Icon { get; }
    public object Header { get; }
    [Reactive]
    public ICommand? Command { get; set; }
    public object? CommandParameter { get; }
    public List<MenuItemVM> Items { get; }

    public MenuItemVM(
        string header,
        IRequirement? checkBoxRequirement = null,
        string? hotkey = null,
        object? commandParameter = null,
        List<MenuItemVM>? items = null)
    {
        if (hotkey is null)
        {
            Header = header;
        }
        else
        {
            Header = new MenuHotkeyHeaderVM
            {
                Header = header,
                Hotkey = hotkey
            };
        }

        CommandParameter = commandParameter;
        Items = items ?? new List<MenuItemVM>();

        if (checkBoxRequirement is null)
        {
            return;
        }

        Icon = new MenuItemCheckBoxVM(checkBoxRequirement);
    }
}