using OpenTracker.Utils.Dialog;
using System.Reflection;

namespace OpenTracker.ViewModels
{
    public class AboutDialogVM : DialogViewModelBase, IAboutDialogVM
    {
        public static string Version { get; } =
            $"v{Assembly.GetExecutingAssembly().GetName().Version}";
    }
}