using OpenTracker.Models.Locations;

namespace OpenTracker.Models.UndoRedo.Notes;

/// <summary>
/// This interface contains the <see cref="IUndoable"/> action to add a note to a <see cref="ILocation"/>.
/// </summary>
public interface IAddNote : IUndoable
{
    /// <summary>
    /// A factory for creating new <see cref="IAddNote"/> objects.
    /// </summary>
    /// <param name="location">
    ///     The <see cref="ILocation"/>.
    /// </param>
    /// <returns>
    ///     A new <see cref="IAddNote"/> object.
    /// </returns>
    delegate IAddNote Factory(ILocation location);
}