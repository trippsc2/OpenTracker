using OpenTracker.Utils.Dialog;
using ReactiveUI;
using System.Reactive;

namespace OpenTracker.ViewModels.Dialogs
{
    /// <summary>
    /// This class contains the message box dialog window ViewModel data.
    /// </summary>
    public class MessageBoxDialogVM : DialogViewModelBase<bool>, IMessageBoxDialogVM
    {
        public string Title { get; }
        public string Text { get; }

        public ReactiveCommand<Unit, Unit> YesCommand { get; }
        public ReactiveCommand<Unit, Unit> NoCommand { get; }
        
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
            Close(true);
        }

        /// <summary>
        /// Selects No to the dialog.
        /// </summary>
        private void No()
        {
            Close();
        }
    }
}
