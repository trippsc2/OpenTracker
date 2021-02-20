using OpenTracker.Utils.Dialog;
using ReactiveUI;
using System.Reactive;

namespace OpenTracker.ViewModels
{
    /// <summary>
    /// This is the ViewModel for the error box dialog window.
    /// </summary>
    public class ErrorBoxDialogVM : DialogViewModelBase
    {
        public ReactiveCommand<Unit, Unit> OkCommand { get; }
        public string Title { get; }
        public string Text { get; }

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
}
