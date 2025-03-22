using OpenTracker.Models.Modes;

namespace OpenTracker.Models.UndoRedo.Mode
{
    /// <summary>
    /// This interface contains the <see cref="IUndoable"/> action to change the <see cref="IMode.KeyDropShuffle"/>
    /// property.
    /// </summary>
    public interface IChangeKeyDropShuffle : IUndoable
    {
        /// <summary>
        /// A factory for creating new <see cref="IChangeKeyDropShuffle"/> objects.
        /// </summary>
        /// <param name="newValue">
        ///     A <see cref="bool"/> representing the new <see cref="IMode.KeyDropShuffle"/> value.
        /// </param>
        /// <returns>
        ///     A new <see cref="IChangeKeyDropShuffle"/> object.
        /// </returns>
        delegate IChangeKeyDropShuffle Factory(bool newValue);
    }
}