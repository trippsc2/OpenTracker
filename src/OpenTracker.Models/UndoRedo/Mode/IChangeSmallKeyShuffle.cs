using OpenTracker.Models.Modes;

namespace OpenTracker.Models.UndoRedo.Mode;

/// <summary>
/// This interface contains undoable action data to change the small key shuffle setting.
/// </summary>
public interface IChangeSmallKeyShuffle : IUndoable
{
    /// <summary>
    /// A factory for creating new <see cref="IChangeSmallKeyShuffle"/> objects.
    /// </summary>
    /// <param name="newValue">
    ///     A <see cref="bool"/> representing the new <see cref="IMode.SmallKeyShuffle"/> value.
    /// </param>
    /// <returns>
    ///     A new <see cref="IChangeSmallKeyShuffle"/> object.
    /// </returns>
    delegate IChangeSmallKeyShuffle Factory(bool newValue);
}