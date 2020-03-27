using OpenTracker.Interfaces;
using OpenTracker.Utils;
using ReactiveUI;
using System;
using System.Reactive;

namespace OpenTracker.ViewModels
{
    public class MessageBoxVM : ViewModelBase, IDialogRequestClose
    {
        public ReactiveCommand<Unit, Unit> YesCommand { get; }
        public ReactiveCommand<Unit, Unit> NoCommand { get; }
        public string Title { get; }
        public string Text { get; }

        public event EventHandler<DialogCloseRequestedEventArgs> CloseRequested;

        public MessageBoxVM(string title, string text)
        {
            YesCommand = ReactiveCommand.Create(Yes);
            NoCommand = ReactiveCommand.Create(No);

            Title = title;
            Text = text;
        }

        private void Yes()
        {
            if (CloseRequested != null)
                CloseRequested.Invoke(this, new DialogCloseRequestedEventArgs(true));
        }

        private void No()
        {
            if (CloseRequested != null)
                CloseRequested.Invoke(this, new DialogCloseRequestedEventArgs(false));
        }
    }
}
