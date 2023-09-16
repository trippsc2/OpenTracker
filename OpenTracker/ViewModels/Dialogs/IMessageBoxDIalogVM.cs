namespace OpenTracker.ViewModels.Dialogs;

public interface IMessageBoxDialogVM
{
    delegate IMessageBoxDialogVM Factory(string title, string text);
}