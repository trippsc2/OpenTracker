using OpenTracker.Models.Modes;

namespace OpenTracker.Models.UndoRedo.Mode;

/// <summary>
/// This interface contains the <see cref="IUndoable"/> action to change the <see cref="IMode.WorldState"/> property.
/// </summary>
public interface IChangeWorldState : IUndoable
{
    /// <summary>
    /// A factory for creating new <see cref="IChangeWorldState"/> objects.
    /// </summary>
    /// <param name="newValue">
    ///     A <see cref="WorldState"/> representing the new <see cref="IMode.WorldState"/> value.
    /// </param>
    /// <returns>
    ///     A new <see cref="IChangeWorldState"/> object.
    /// </returns>
    delegate IChangeWorldState Factory(WorldState newValue);
}