using OpenTracker.Utils.Dialog;
using System.Reflection;

namespace OpenTracker.ViewModels
{
    public class AboutDialogVM : DialogViewModelBase, IAboutDialogVM
    {
        public static string Version =>
            $"v{Assembly.GetExecutingAssembly().GetName().Version}";
    }
}