using OpenTracker.Models.Locations;

namespace OpenTracker.Models.UndoRedo.Locations;

/// <summary>
/// This interface contains the <see cref="IUndoable"/> action to unpin a <see cref="ILocation"/>.
/// </summary>
public interface IUnpinLocation : IUndoable
{
    /// <summary>
    /// A factory for creating new <see cref="IUnpinLocation"/> objects.
    /// </summary>
    /// <param name="location">
    ///     The <see cref="ILocation"/>.
    /// </param>
    /// <returns>
    ///     A new <see cref="IUnpinLocation"/> object.
    /// </returns>
    delegate IUnpinLocation Factory(ILocation location);
}