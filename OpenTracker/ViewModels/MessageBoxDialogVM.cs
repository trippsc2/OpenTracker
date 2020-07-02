using OpenTracker.Interfaces;
using OpenTracker.Utils;
using OpenTracker.ViewModels.Bases;
using ReactiveUI;
using System;
using System.Reactive;

namespace OpenTracker.ViewModels
{
    public class MessageBoxDialogVM : ViewModelBase, IDialogRequestClose
    {
        public ReactiveCommand<Unit, Unit> YesCommand { get; }
        public ReactiveCommand<Unit, Unit> NoCommand { get; }
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
        public MessageBoxDialogVM(string title, string text)
        {
            YesCommand = ReactiveCommand.Create(Yes);
            NoCommand = ReactiveCommand.Create(No);

            Title = title;
            Text = text;
        }

        /// <summary>
        /// Selects Yes to the dialog.
        /// </summary>
        private void Yes()
        {
            CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(true));
        }

        /// <summary>
        /// Selects No to the dialog.
        /// </summary>
        private void No()
        {
            CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(false));
        }
    }
}
