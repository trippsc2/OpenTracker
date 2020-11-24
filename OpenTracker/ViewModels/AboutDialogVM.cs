using OpenTracker.Interfaces;
using OpenTracker.Utils;
using System;
using System.Reflection;

namespace OpenTracker.ViewModels
{
    public class AboutDialogVM : ViewModelBase, IDialogRequestClose
    {
        public event EventHandler<DialogCloseRequestedEventArgs> CloseRequested;

        public static string Version =>
            $"v{Assembly.GetExecutingAssembly().GetName().Version}";
    }
}