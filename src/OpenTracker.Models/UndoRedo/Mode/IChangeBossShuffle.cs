using OpenTracker.Models.Modes;

namespace OpenTracker.Models.UndoRedo.Mode
{
    /// <summary>
    /// This interface contains the <see cref="IUndoable"/> action to change the <see cref="IMode.BossShuffle"/>
    /// property.
    /// </summary>
    public interface IChangeBossShuffle : IUndoable
    {
        /// <summary>
        /// A factory for creating new <see cref="IChangeBossShuffle"/> objects.
        /// </summary>
        /// <param name="newValue">
        ///     A <see cref="bool"/> representing the new <see cref="IMode.BossShuffle"/> value.
        /// </param>
        /// <returns>
        ///     A new <see cref="IChangeBossShuffle"/> object.
        /// </returns>
        delegate IChangeBossShuffle Factory(bool newValue);
    }
}