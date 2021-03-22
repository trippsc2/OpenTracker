using ReactiveUI;

namespace OpenTracker.Models.UndoRedo
{
    public interface IUndoRedoManager : IReactiveObject
    {
        bool CanRedo { get; }
        bool CanUndo { get; }

        delegate IUndoRedoManager Factory();

        void NewAction(IUndoable action);
        void Redo();
        void Reset();
        void Undo();
    }
}