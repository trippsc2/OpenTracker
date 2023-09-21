using OpenTracker.Utils;
using OpenTracker.Utils.Autofac;

namespace OpenTracker.ViewModels.Menus;

[DependencyInjection]
public sealed class MenuHotkeyHeaderVM : ViewModel
{
    public required string Hotkey { get; init; }
    public required string Header { get; init; }
}