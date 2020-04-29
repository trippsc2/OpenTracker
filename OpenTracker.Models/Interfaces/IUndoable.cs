namespace OpenTracker.Models.Interfaces
{
    public interface IUndoable
    {
        void Execute();
        void Undo();
    }
}
