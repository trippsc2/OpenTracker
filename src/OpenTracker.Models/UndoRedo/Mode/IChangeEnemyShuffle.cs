using OpenTracker.Models.Modes;

namespace OpenTracker.Models.UndoRedo.Mode
{
    /// <summary>
    /// This interface contains the <see cref="IUndoable"/> action to change the <see cref="IMode.EnemyShuffle"/>
    /// property.
    /// </summary>
    public interface IChangeEnemyShuffle : IUndoable
    {
        /// <summary>
        /// A factory for creating new <see cref="IChangeEnemyShuffle"/> objects.
        /// </summary>
        /// <param name="newValue">
        ///     A <see cref="bool"/> representing the new <see cref="IMode.EnemyShuffle"/> value.
        /// </param>
        /// <returns>
        ///     A new <see cref="IChangeEnemyShuffle"/> object.
        /// </returns>
        delegate IChangeEnemyShuffle Factory(bool newValue);
    }
}