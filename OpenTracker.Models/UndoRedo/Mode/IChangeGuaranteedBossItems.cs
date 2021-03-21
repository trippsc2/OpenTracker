namespace OpenTracker.Models.UndoRedo.Mode
{
    /// <summary>
    /// This interface contains undoable action to change the guaranteed boss items setting (Ambrosia).
    /// </summary>
    public interface IChangeGuaranteedBossItems : IUndoable
    {
        delegate IChangeGuaranteedBossItems Factory(bool newValue);
    }
}