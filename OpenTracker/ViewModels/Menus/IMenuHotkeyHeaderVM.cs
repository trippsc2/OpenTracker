namespace OpenTracker.ViewModels.Menus;

public interface IMenuHotkeyHeaderVM
{
    string Hotkey { get; }
    string Header { get; }
        
    delegate IMenuHotkeyHeaderVM Factory(string hotkey, string header);
}