using OpenTracker.Models.Modes;

namespace OpenTracker.Models.UndoRedo.Mode
{
    /// <summary>
    /// This interface contains the <see cref="IUndoable"/> action to change the <see cref="IMode.GenericKeys"/>
    /// property.
    /// </summary>
    public interface IChangeGenericKeys : IUndoable
    {
        /// <summary>
        /// A factory for creating new <see cref="IChangeGenericKeys"/> objects.
        /// </summary>
        /// <param name="newValue">
        ///     A <see cref="bool"/> representing the new <see cref="IMode.GenericKeys"/> value.
        /// </param>
        /// <returns>
        ///     A new <see cref="IChangeGenericKeys"/> object.
        /// </returns>
        delegate IChangeGenericKeys Factory(bool newValue);
    }
}