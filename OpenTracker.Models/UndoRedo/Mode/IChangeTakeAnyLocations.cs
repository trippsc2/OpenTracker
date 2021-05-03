namespace OpenTracker.Models.UndoRedo.Mode
{
    /// <summary>
    /// This interface contains undoable action data to change the take any locations mode setting.
    /// </summary>
    public interface IChangeTakeAnyLocations : IUndoable
    {
        delegate IChangeTakeAnyLocations Factory(bool newValue);
    }
}