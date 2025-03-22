using OpenTracker.Models.Modes;

namespace OpenTracker.Models.UndoRedo.Mode
{
    /// <summary>
    /// This interface contains the <see cref="IUndoable"/> action to change the <see cref="IMode.BigKeyShuffle"/>
    /// property.
    /// </summary>
    public interface IChangeBigKeyShuffle : IUndoable
    {
        /// <summary>
        /// A factory for creating new <see cref="IChangeBigKeyShuffle"/> objects.
        /// </summary>
        /// <param name="newValue">
        ///     A <see cref="bool"/> representing the new <see cref="IMode.BigKeyShuffle"/> value.
        /// </param>
        /// <returns>
        ///     A new <see cref="IChangeBigKeyShuffle"/> object.
        /// </returns>
        delegate IChangeBigKeyShuffle Factory(bool newValue);
    }
}