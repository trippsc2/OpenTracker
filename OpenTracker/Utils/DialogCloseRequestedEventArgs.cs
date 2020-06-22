using System;

namespace OpenTracker.Utils
{
    /// <summary>
    /// This is the class for event arguments for requesting a dialog
    /// window close.
    /// </summary>
    public class DialogCloseRequestedEventArgs : EventArgs
    {
        public bool? DialogResult { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dialogResult">
        /// A nullable boolean representing the dialog result.
        /// </param>
        public DialogCloseRequestedEventArgs(bool? dialogResult)
        {
            DialogResult = dialogResult;
        }
    }
}
