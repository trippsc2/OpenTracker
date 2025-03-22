using OpenTracker.Models.Modes;

namespace OpenTracker.Models.UndoRedo.Mode
{
    /// <summary>
    /// This interface contains the <see cref="IUndoable"/> action to change the <see cref="IMode.MapShuffle"/>
    /// property.
    /// </summary>
    public interface IChangeMapShuffle : IUndoable
    {
        /// <summary>
        /// A factory for creating new <see cref="IChangeMapShuffle"/> objects.
        /// </summary>
        /// <param name="newValue">
        ///     A <see cref="bool"/> representing the new <see cref="IMode.MapShuffle"/> value.
        /// </param>
        /// <returns>
        ///     A new <see cref="IChangeMapShuffle"/> object.
        /// </returns>
        delegate IChangeMapShuffle Factory(bool newValue);
    }
}