namespace OpenTracker.ViewModels
{
    public interface IErrorBoxDialogVM
    {
        delegate IErrorBoxDialogVM Factory(string title, string text);
    }
}
