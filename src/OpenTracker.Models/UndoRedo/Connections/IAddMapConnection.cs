using OpenTracker.Models.Locations.Map.Connections;

namespace OpenTracker.Models.UndoRedo.Connections;

/// <summary>
/// This interface contains the <see cref="IUndoable"/> action to add a new <see cref="IMapConnection"/> to the map.
/// </summary>
public interface IAddMapConnection : IUndoable
{
    /// <summary>
    /// A factory for creating new <see cref="IAddMapConnection"/> objects.
    /// </summary>
    /// <param name="mapConnection">
    ///     The <see cref="IMapConnection"/>.
    /// </param>
    /// <returns>
    ///     A new <see cref="IAddMapConnection"/> object.
    /// </returns>
    delegate IAddMapConnection Factory(IMapConnection mapConnection);
}