using OpenTracker.Utils;
using System;

namespace OpenTracker.Interfaces
{
    public interface IDialogRequestClose
    {
        event EventHandler<DialogCloseRequestedEventArgs> CloseRequested;
    }
}
