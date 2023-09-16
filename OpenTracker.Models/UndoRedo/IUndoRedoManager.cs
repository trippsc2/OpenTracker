using OpenTracker.Models.Reset;
using ReactiveUI;

namespace OpenTracker.Models.UndoRedo;

/// <summary>
/// This interface contains logic managing <see cref="IUndoable"/> actions.
/// </summary>
public interface IUndoRedoManager : IReactiveObject, IResettable
{
    /// <summary>
    /// A boolean representing whether an action can be undone.
    /// </summary>
    bool CanUndo { get; }

    /// <summary>
    /// A boolean representing whether an action can be redone.
    /// </summary>
    bool CanRedo { get; }

    /// <summary>
    /// A factory for creating the <see cref="IUndoRedoManager"/> object.
    /// </summary>
    /// <returns>
    ///     The <see cref="IUndoRedoManager"/> object.
    /// </returns>
    delegate IUndoRedoManager Factory();

    /// <summary>
    /// Executes the specified <see cref="IUndoable"/> action and adds it to the stack of undoable actions.
    /// </summary>
    /// <param name="action">
    ///     The <see cref="IUndoable"/> action.
    /// </param>
    void NewAction(IUndoable action);

    /// <summary>
    /// Undo the last action.
    /// </summary>
    void Undo();

    /// <summary>
    /// Redo the last undone action.
    /// </summary>
    void Redo();
}