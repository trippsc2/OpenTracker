using OpenTracker.Utils;
using System;

namespace OpenTracker.Interfaces
{
    /// <summary>
    /// This is the interface providing access to request closing a dialog
    /// window from the view-model.
    /// </summary>
    public interface IDialogRequestClose
    {
        event EventHandler<DialogCloseRequestedEventArgs> CloseRequested;
    }
}
