namespace OpenTracker.ViewModels.Dialogs
{
    public interface IErrorBoxDialogVM
    {
        delegate IErrorBoxDialogVM Factory(string title, string text);
    }
}
