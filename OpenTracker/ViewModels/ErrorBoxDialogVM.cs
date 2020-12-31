using OpenTracker.Interfaces;
using OpenTracker.Utils;
using ReactiveUI;
using System;
using System.Reactive;

namespace OpenTracker.ViewModels
{
    /// <summary>
    /// This is the ViewModel for the error box dialog window.
    /// </summary>
    public class ErrorBoxDialogVM : ViewModelBase, IDialogRequestClose
    {
        public ReactiveCommand<Unit, Unit> OkCommand { get; }
        public string Title { get; }
        public string Text { get; }

        public event EventHandler<DialogCloseRequestedEventArgs> CloseRequested;

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
            CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(true));
        }
    }
}
