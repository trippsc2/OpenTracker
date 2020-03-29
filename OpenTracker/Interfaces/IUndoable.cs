namespace OpenTracker.Interfaces
{
    public interface IUndoable
    {
        void Execute();
        void Undo();
    }
}
