using OpenTracker.Models.Modes;

namespace OpenTracker.Models.UndoRedo.Mode
{
    /// <summary>
    /// This interface contains the <see cref="IUndoable"/> action to change the <see cref="IMode.CompassShuffle"/>
    /// property.
    /// </summary>
    public interface IChangeCompassShuffle : IUndoable
    {
        /// <summary>
        /// A factory for creating new <see cref="IChangeCompassShuffle"/> objects.
        /// </summary>
        /// <param name="newValue">
        ///     A <see cref="bool"/> representing the new <see cref="IMode.CompassShuffle"/> value.
        /// </param>
        /// <returns>
        ///     A new <see cref="IChangeCompassShuffle"/> object.
        /// </returns>
        delegate IChangeCompassShuffle Factory(bool newValue);
    }
}