using System.Reflection;
using OpenTracker.Utils.Dialog;

namespace OpenTracker.ViewModels.Dialogs
{
    public class AboutDialogVM : DialogViewModelBase, IAboutDialogVM
    {
        public static string Version { get; } = $"v{Assembly.GetExecutingAssembly().GetName().Version}";
    }
}