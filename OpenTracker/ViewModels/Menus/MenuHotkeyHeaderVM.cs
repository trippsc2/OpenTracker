using OpenTracker.Utils;

namespace OpenTracker.ViewModels.Menus
{
    public class MenuHotkeyHeaderVM : ViewModelBase, IMenuHotkeyHeaderVM
    {
        public string Hotkey { get; }
        public string Header { get; }

        public MenuHotkeyHeaderVM(string hotkey, string header)
        {
            Hotkey = hotkey;
            Header = header;
        }
    }
}