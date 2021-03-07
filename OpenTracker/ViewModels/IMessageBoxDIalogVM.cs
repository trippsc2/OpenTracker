namespace OpenTracker.ViewModels
{
    public interface IMessageBoxDialogVM
    {
        delegate IMessageBoxDialogVM Factory(string title, string text);
    }
}
