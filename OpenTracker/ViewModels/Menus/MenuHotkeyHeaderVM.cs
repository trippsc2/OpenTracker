using OpenTracker.Autofac;
using OpenTracker.Utils;

namespace OpenTracker.ViewModels.Menus;

[DependencyInjection]
public sealed class MenuHotkeyHeaderVM : ViewModel, IMenuHotkeyHeaderVM
{
    public string Hotkey { get; }
    public string Header { get; }

    public MenuHotkeyHeaderVM(string hotkey, string header)
    {
        Hotkey = hotkey;
        Header = header;
    }
}