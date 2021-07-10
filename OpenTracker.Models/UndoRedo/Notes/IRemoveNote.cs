using OpenTracker.Models.Locations;
using OpenTracker.Models.Markings;

namespace OpenTracker.Models.UndoRedo.Notes
{
    /// <summary>
    /// This interface contains the <see cref="IUndoable"/> action to remove a note to a <see cref="ILocation"/>.
    /// </summary>
    public interface IRemoveNote : IUndoable
    {
        /// <summary>
        /// A factory for creating new <see cref="IRemoveNote"/> objects.
        /// </summary>
        /// <param name="note">
        ///     The <see cref="IMarking"/> representing the note to be removed.
        /// </param>
        /// <param name="location">
        ///     The <see cref="ILocation"/>.
        /// </param>
        /// <returns>
        ///     A new <see cref="IRemoveNote"/> object.
        /// </returns>
        delegate IRemoveNote Factory(IMarking note, ILocation location);
    }
}