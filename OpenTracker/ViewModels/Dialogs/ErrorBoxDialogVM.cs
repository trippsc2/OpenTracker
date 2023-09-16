using System.Reactive;
using OpenTracker.Utils.Dialog;
using ReactiveUI;

namespace OpenTracker.ViewModels.Dialogs;

/// <summary>
/// This is the ViewModel for the error box dialog window.
/// </summary>
public class ErrorBoxDialogVM : DialogViewModelBase, IErrorBoxDialogVM
{
    public string Title { get; }
    public string Text { get; }

    public ReactiveCommand<Unit, Unit> OkCommand { get; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="title">
    /// A string representing the window title.
    /// </param>
    /// <param name="text">
    /// A string representing the dialog text.
    /// </param>
    public ErrorBoxDialogVM(string title, string text)
    {
        OkCommand = ReactiveCommand.Create(Ok);

        Title = title;
        Text = text;
    }

    /// <summary>
    /// Selects Ok to the dialog.
    /// </summary>
    private void Ok()
    {
        Close();
    }
}