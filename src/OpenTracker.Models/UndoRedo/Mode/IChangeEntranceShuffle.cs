using OpenTracker.Models.Modes;

namespace OpenTracker.Models.UndoRedo.Mode;

/// <summary>
/// This interface contains the <see cref="IUndoable"/> action to change the <see cref="IMode.EntranceShuffle"/>
/// property.
/// </summary>
public interface IChangeEntranceShuffle : IUndoable
{
    /// <summary>
    /// A factory for creating new <see cref="IChangeEntranceShuffle"/> objects.
    /// </summary>
    /// <param name="newValue">
    ///     A <see cref="EntranceShuffle"/> representing the new <see cref="IMode.EntranceShuffle"/> value.
    /// </param>
    /// <returns>
    ///     A new <see cref="IChangeEntranceShuffle"/> object.
    /// </returns>
    delegate IChangeEntranceShuffle Factory(EntranceShuffle newValue);
}