namespace OpenTracker.Models.UndoRedo.Mode
{
    /// <summary>
    /// This interface contains undoable action data to change the generic keys mode setting.
    /// </summary>
    public interface IChangeGenericKeys : IUndoable
    {
        delegate IChangeGenericKeys Factory(bool newValue);
    }
}