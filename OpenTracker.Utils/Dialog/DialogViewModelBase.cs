using System;

namespace OpenTracker.Utils.Dialog
{
    /// <summary>
    /// This is the base class for all dialog window ViewModels.
    /// </summary>
    /// <typeparam name="TResult">
    /// This is the type for reporting the result of the dialog box.
    /// </typeparam>
    public class DialogViewModelBase<TResult> : ViewModelBase
    {
        public bool IsOpen { get; set; }

        public event EventHandler<DialogResultEventArgs<TResult>>? CloseRequested;

        /// <summary>
        /// Requests the dialog window to close.
        /// </summary>
        /// <param name="result">
        /// The result of the dialog window.
        /// </param>
        protected void Close(TResult result = default!)
        {
            CloseRequested?.Invoke(this, new DialogResultEventArgs<TResult>(result!));
        }
    }

    /// <summary>
    /// This is the base class for all dialog window ViewModels that don't require a result.
    /// </summary>
    public class DialogViewModelBase : DialogViewModelBase<object>
    {
    }
}
