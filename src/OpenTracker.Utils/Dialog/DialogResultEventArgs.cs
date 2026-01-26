using System;

namespace OpenTracker.Utils.Dialog;

/// <summary>
/// This is the class for event args for returning a result from an dialog window.
/// </summary>
/// <typeparam name="TResult">
/// The type of the result.
/// </typeparam>
public class DialogResultEventArgs<TResult> : EventArgs
{
    public TResult Result { get; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="result">
    /// The result of the dialog window.
    /// </param>
    public DialogResultEventArgs(TResult result)
    {
        Result = result;
    }
}