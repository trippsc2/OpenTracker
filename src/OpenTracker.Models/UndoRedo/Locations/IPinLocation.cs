using OpenTracker.Models.Locations;

namespace OpenTracker.Models.UndoRedo.Locations;

/// <summary>
/// This interface contains the <see cref="IUndoable"/> action to pin a <see cref="ILocation"/>.
/// </summary>
public interface IPinLocation : IUndoable
{
    /// <summary>
    /// A factory for creating new <see cref="IPinLocation"/> objects.
    /// </summary>
    /// <param name="location">
    ///     The <see cref="ILocation"/>.
    /// </param>
    /// <returns>
    ///     A new <see cref="IPinLocation"/> object.
    /// </returns>
    delegate IPinLocation Factory(ILocation location);
}