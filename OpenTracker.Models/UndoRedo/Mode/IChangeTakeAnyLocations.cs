using OpenTracker.Models.Modes;

namespace OpenTracker.Models.UndoRedo.Mode
{
    /// <summary>
    /// This interface contains the <see cref="IUndoable"/> action to change the <see cref="IMode.TakeAnyLocations"/>
    /// property.
    /// </summary>
    public interface IChangeTakeAnyLocations : IUndoable
    {
        /// <summary>
        /// A factory for creating new <see cref="IChangeTakeAnyLocations"/> objects.
        /// </summary>
        /// <param name="newValue">
        ///     A <see cref="bool"/> representing the new <see cref="IMode.TakeAnyLocations"/> value.
        /// </param>
        /// <returns>
        ///     A new <see cref="IChangeTakeAnyLocations"/> object.
        /// </returns>
        delegate IChangeTakeAnyLocations Factory(bool newValue);
    }
}